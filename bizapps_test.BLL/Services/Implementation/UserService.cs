using System;
using System.CodeDom;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Mappers;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IBlogService _blogService;
        private readonly ICustomLogger _logger;
        private readonly IBlogUserBuilder _blogUserBuilder;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepositoryFactory _repositoryFactory;

        public UserService(IBlogService blogService,  IUnitOfWorkFactory uowFactory,
            IRepositoryFactory repositoryFactory, ILogFactory logFactory)
        {
            _blogUserBuilder  = new BlogUserBuilder();
            _blogService = blogService;
            _logger = logFactory.CreateLogger(GetType());
            _repositoryFactory = repositoryFactory;
            _unitOfWorkFactory = uowFactory;
        }        



        public AnswerStatus CreateUser(BlogUserDto userDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var userToCreate = _blogUserBuilder.CreateBlogUser(userDTO.UserName, userDTO.UserPassword);
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);
                    int createdUserId = userRepository.CreateEntity(userToCreate);
                    
                    if (userDTO.UserBlog != null)
                    {
                        userDTO.Id = createdUserId;
                        userDTO.UserBlog.CreatedBy = userDTO;
                        var blogRepository = _repositoryFactory.CreateBlogRepository(uow);

                        var result = _blogService.CreateBlog(userDTO.UserBlog, blogRepository);

                        if (result == AnswerStatus.Failed)
                        {
                            return AnswerStatus.Failed;
                        }
                    }

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



        public AnswerStatus UpdateUser(BlogUserDto userDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var userToUpdate = _blogUserBuilder.CreateBlogUser(userDTO.UserName, userDTO.UserPassword);
                    _blogUserBuilder.SetBlogUserId(userToUpdate, userDTO.Id);
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);
                    userRepository.UpdateEntity(userToUpdate);

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


        public AnswerStatus DeleteUser(BlogUserDto userDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    if (userDTO.Id <= 0)
                    {
                        throw new ArgumentException("id can't be less or equal to 0");
                    }
                    
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);

                    var userToDelete = userRepository.GetEntityById(userDTO.Id); 

                    var removeBlogResult = RemoveUserBlog(userDTO.Id, uow);
                    if (removeBlogResult == AnswerStatus.Failed)
                    {
                        return removeBlogResult;
                    }

                    var removeCommentResult = RemoveUserComments(userDTO.Id, uow);
                    if (removeCommentResult == AnswerStatus.Failed)
                    {
                        return removeCommentResult;
                    }

                    userRepository.DeleteEntity(userToDelete);

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

        private AnswerStatus RemoveUserBlog(int userId, IUnitOfWork uow)
        {
            var blogServiceAnswer = _blogService.GetBlogByUserId(userId);
            if ((blogServiceAnswer.Status == AnswerStatus.Failed))
            {
                return AnswerStatus.Failed;
            }

            if ((blogServiceAnswer.ReceivedEntity != null))
            {
                
                var blogRepository = _repositoryFactory.CreateBlogRepository(uow);
                AnswerStatus status = _blogService.DeleteBlog(blogServiceAnswer.ReceivedEntity, blogRepository);
                if (status == AnswerStatus.Failed)
                {
                    return AnswerStatus.Failed;
                }
            }

            return AnswerStatus.Successfull;
        }


        private AnswerStatus RemoveUserComments(int userId, IUnitOfWork uow)
        {
            var commentRepository = _repositoryFactory.CreateCommentRepository(uow);
            var userComments =  commentRepository.GetUserComments(userId);

            if (userComments != null)
            {
                foreach (var comment in userComments)
                {
                    commentRepository.DeleteEntity(comment);
                }
            }
        

            return AnswerStatus.Successfull;
        }


        public ServiceAnswer<BlogUserDto> GetUserById(int userId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);

                    var receivedUser = userRepository.GetEntityById(userId);

                    var userDTO = new BlogUserDto();

                    if (receivedUser != null)
                    {
                        userDTO = MapReceivedUser(receivedUser);
                    }
                    
                    return new ServiceAnswer<BlogUserDto>(userDTO, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<BlogUserDto>(null, AnswerStatus.Failed);
                }
            }         
        }


        public ServiceAnswer<BlogUserDto> GetUserByName(string userName)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var userRepository = _repositoryFactory.CreateUserRepository(uow);

                    var receivedUser = userRepository.GetBlogUserByName(userName);

                    var userDTO = new BlogUserDto();

                    if (receivedUser != null)
                    {
                        userDTO = MapReceivedUser(receivedUser);
                    }

                    return new ServiceAnswer<BlogUserDto>(userDTO, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<BlogUserDto>(null, AnswerStatus.Failed);
                }
            }          
        }

        private BlogUserDto MapReceivedUser(BlogUser userMapFrom)
        {
            var userMapper = new BlogUserMapper();
            var blogMapper = new BlogMapper();
            BlogUserDto userDTO = userMapper.MapToBlogUserDto(userMapFrom);
            if (userMapFrom.UserBlog != null)
            {
                userDTO.UserBlog = blogMapper.MapToBlogDto(userMapFrom.UserBlog);
            }

            return userDTO;
        }
    }
}