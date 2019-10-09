using System;
using System.Collections.Generic;
using System.Linq;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Mappers;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Services.Implementation
{
    public class CommentService: ICommentService
    {

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IRepositoryFactory _repositoryFactory;

        private readonly ICustomLogger _logger;

        private readonly ICommentBuilder _commentBuilder;


        public CommentService(IUnitOfWorkFactory uowFactory, IRepositoryFactory repositoryFactory, ILogFactory logFactory)
        {
            _unitOfWorkFactory = uowFactory;
            _repositoryFactory = repositoryFactory;
            _commentBuilder = new CommentBuilder();
            _logger = logFactory.CreateLogger(GetType());
        }

        public AnswerStatus CreateComment(CommentDto commentDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var postRepository = _repositoryFactory.CreatePostRepository(uow);
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);

                    var commentToCreate = _commentBuilder.CreateComment(commentDTO.CommentText, commentDTO.CreationDate);
                    var postRelatedTo = postRepository.GetEntityById(commentDTO.RelatedTo.Id);
                    _commentBuilder.SetPostRelatedTo(commentToCreate, postRelatedTo);
                    var userCreatedBy = userRepository.GetEntityById(commentDTO.CreatedBy.Id);
                    _commentBuilder.SetUserCreatedBy(commentToCreate, userCreatedBy);

                    var commentRepository = _repositoryFactory.CreateCommentRepository(uow);
                    commentRepository.CreateEntity(commentToCreate);

                    uow.SaveChanges();

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }
            }
        }

        public AnswerStatus UpdateComment(CommentDto commentDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var commentToUpdate = _commentBuilder.CreateComment(commentDTO.CommentText, commentDTO.CreationDate);
                    _commentBuilder.SetCommentId(commentToUpdate, commentDTO.Id);

                    var commentRepository = _repositoryFactory.CreateCommentRepository(uow);
                    commentRepository.UpdateEntity(commentToUpdate);

                    uow.SaveChanges();

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }
            }
        }

        public AnswerStatus DeleteComment(CommentDto commentDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    if (commentDTO.Id <= 0)
                    {
                        throw new ArgumentException("id can't be less or equal to 0");
                    }

                    var commentRepository = _repositoryFactory.CreateCommentRepository(uow);

                    var commentToDelete = commentRepository.GetEntityById(commentDTO.Id);

                    commentRepository.DeleteEntity(commentToDelete);

                    uow.SaveChanges();
                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }
            }
        }


        public ServiceAnswer<IEnumerable<CommentDto>> GetPostComments(int postId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var commentRepository = _repositoryFactory.CreateCommentRepository(uow);

                    var receivedComments = commentRepository.GetPostComments(postId);

                    var commentDTOList = new List<CommentDto>();

                    if (receivedComments != null)
                    {
                        commentDTOList = MapRecievedCommentList(receivedComments);
                    }

                    return new ServiceAnswer<IEnumerable<CommentDto>>(commentDTOList, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<IEnumerable<CommentDto>>(null, AnswerStatus.Failed);
                }
            }
        }


        public ServiceAnswer<CommentDto> GetCommentById(int commentId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var commentRepository = _repositoryFactory.CreateCommentRepository(uow);

                    var receivedComment = commentRepository.GetEntityById(commentId);

                    var commentDTO = new CommentDto();

                    if (receivedComment != null)
                    {
                        commentDTO = MapReceivedComment(receivedComment);
                    }

                    return new ServiceAnswer<CommentDto>(commentDTO, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<CommentDto>(null, AnswerStatus.Failed);
                }
            }
        }

        private List<CommentDto> MapRecievedCommentList(IEnumerable<Comment> commentListMapFrom)
        {

            var commentDTOList = new List<CommentDto>();
            foreach (var comment in commentListMapFrom)
            {
                commentDTOList.Add(MapReceivedComment(comment));
            }

            return commentDTOList;
        }


        private CommentDto MapReceivedComment(Comment commentMapFrom)
        {
            var commentMapper = new CommentMapper();         

            var commentDTO = commentMapper.MapToCommentDto(commentMapFrom);

            if (commentMapFrom.CreatedBy != null)
            {
                var userMapper = new BlogUserMapper();
                commentDTO.CreatedBy = userMapper.MapToBlogUserDto(commentMapFrom.CreatedBy);
            }

            if (commentMapFrom.RelatedTo != null)
            {
                var postMapper = new PostMapper();
                commentDTO.RelatedTo = postMapper.MapToPostDto(commentMapFrom.RelatedTo);
            }

            return commentDTO;
        }

    }
}