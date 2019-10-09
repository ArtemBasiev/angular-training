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
    public class PostService: IPostService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IRepositoryFactory _repositoryFactory;

        private readonly ICustomLogger _logger;

        private readonly IBlogBuilder _blogBuilder;

        private readonly IPostBuilder _postBuilder;


        public PostService(IUnitOfWorkFactory uowFactory, IRepositoryFactory repositoryFactory, ILogFactory logFactory)
        {
            _unitOfWorkFactory = uowFactory;
            _repositoryFactory = repositoryFactory;
            _blogBuilder = new BlogBuilder();
            _postBuilder = new PostBuilder();
            _logger = logFactory.CreateLogger(GetType());
        }


        public AnswerStatus CreatePost(PostDto postDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var postToCreate = _postBuilder.CreatePost(postDTO.PostTitle, postDTO.PostContent, postDTO.CreationDate);
                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);
                    var postBlog = blogRepository.GetEntityById(postDTO.RelatedTo.Id);
                    _postBuilder.SetBlogRelatedTo(postToCreate, postBlog);

                    var postRepository = _repositoryFactory.CreatePostRepository(uow);
                    postRepository.CreateEntity(postToCreate);

                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);
                    if (postDTO.PostCategories != null)
                    {
                        foreach (var categoryDTO in postDTO.PostCategories)
                        {
                            var categoryToAdd = categoryRepository.GetEntityById(categoryDTO.Id);
                            if (categoryToAdd != null)
                            {
                                postRepository.AddCategoryToPost(postToCreate, categoryToAdd);
                            }                           
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

        public AnswerStatus UpdatePost(PostDto postDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var postToUpdate = _postBuilder.CreatePost(postDTO.PostTitle, postDTO.PostContent, postDTO.CreationDate);
                    _postBuilder.SetPostId(postToUpdate, postDTO.Id);

                    var postRepository = _repositoryFactory.CreatePostRepository(uow);
                    postRepository.UpdateEntity(postToUpdate);

                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);

                    var postCategories = categoryRepository.GetPostCategories(postDTO.Id);

                    int isEqual = 0;

                    var postCategoriesDto = new List<CategoryDto>();

                    var postCategoriesToAdd = new List<CategoryDto>();

                    if (postDTO.PostCategories != null)
                    {
                        postCategoriesDto = postDTO.PostCategories.ToList();

                        postCategoriesToAdd = postDTO.PostCategories.ToList();

                    }
                    
                    foreach (var postCategory in postCategories)
                    {
                        isEqual = 0;
                        foreach (var categoryDto in postCategoriesDto)
                        {
                            if (categoryDto.Id == postCategory.Id)
                            {
                                postCategoriesToAdd.RemoveAll(c=>c.Id == postCategory.Id);
                                isEqual = 1;
                            }
                        }

                        if (isEqual == 0)
                        {
                            postRepository.RemoveCategoryFromPost(postToUpdate, postCategory);
                        }

                    }

                    foreach (var categoryDTO in postCategoriesToAdd)
                    {
                        var categoryToAdd = categoryRepository.GetEntityById(categoryDTO.Id);
                        postRepository.AddCategoryToPost(postToUpdate, categoryToAdd);
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



        public AnswerStatus DeletePost(PostDto postDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var postRepository = _repositoryFactory.CreatePostRepository(uow);

                var result =  DeletePost(postDTO, postRepository);
                if (result == AnswerStatus.Failed)
                {
                    return result;
                }

                uow.SaveChanges();
                return AnswerStatus.Successfull;
            }
        }


        public AnswerStatus DeletePost(PostDto postDTO, IPostRepository postRepository)
        {
                try
                {
                    if (postDTO.Id <= 0)
                    {
                        throw new ArgumentException("id can't be less or equal to 0");
                    }

                    var postToDelete = postRepository.GetEntityById(postDTO.Id);

                    var deleteCommentsResult = DeletePostComments(postToDelete.Id, postRepository.UnitOfWork);
                    if (deleteCommentsResult == AnswerStatus.Failed)
                    {
                        return deleteCommentsResult;
                    }

                    RemoveCategoriesFromPost(postToDelete, postRepository);

                    postRepository.DeleteEntity(postToDelete);

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }
            
        }

        private AnswerStatus DeletePostComments(int postId, IUnitOfWork uow)
        {
            var commentRepository = _repositoryFactory.CreateCommentRepository(uow);

            var postComments = commentRepository.GetPostComments(postId);

            if (postComments != null)
            {
                foreach (var comment in postComments)
                {
                    commentRepository.DeleteEntity(comment);

                }
            }
            
            return AnswerStatus.Successfull;
        }


        private void RemoveCategoriesFromPost(Post postToDelete, IPostRepository postRepository)
        {
            var categoryRepository = _repositoryFactory.CreateCategoryRepository(postRepository.UnitOfWork);

            var postCategories = categoryRepository.GetPostCategories(postToDelete.Id);

            foreach (var category in postCategories)
            {
                postRepository.RemoveCategoryFromPost(postToDelete, category);
            }
        }


        public ServiceAnswer<IEnumerable<PostDto>> GetBlogPosts(int blogId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var postRepository = _repositoryFactory.CreatePostRepository(uow);
                
                    var receivedPosts = postRepository.GetPostsByBlogId(blogId);

                    var postDTOList = new List<PostDto>();

                    if (receivedPosts != null)
                    {
                        postDTOList = MapRecievedPostList(receivedPosts);
                    }

                    return new ServiceAnswer<IEnumerable<PostDto>>(postDTOList, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<IEnumerable<PostDto>>(null, AnswerStatus.Failed);
                }
            }
        }

        public ServiceAnswer<PostDto> GetPostById(int postId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var postRepository = _repositoryFactory.CreatePostRepository(uow);

                    var receivedPost = postRepository.GetEntityById(postId);

                    var postDTO = new PostDto();

                    if (receivedPost != null)
                    {
                        postDTO = MapReceivedPost(receivedPost);
                    }

                    return new ServiceAnswer<PostDto>(postDTO, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<PostDto>(null, AnswerStatus.Failed);
                }
            }
        }

        private List<PostDto> MapRecievedPostList(IEnumerable<Post> postListMapFrom)
        {
            var postDTOList = new List<PostDto>();
            foreach (var post in postListMapFrom)
            {
                postDTOList.Add(MapReceivedPost(post));
            }

            return postDTOList;
        }


        private PostDto MapReceivedPost(Post postMapFrom)
        {     
            var postMapper = new PostMapper();

            PostDto postDTO = postMapper.MapToPostDto(postMapFrom);

            if (postMapFrom.RelatedTo != null)
            {
                var blogMapper = new BlogMapper();
                postDTO.RelatedTo = blogMapper.MapToBlogDto(postMapFrom.RelatedTo);
            }

            if (postMapFrom.PostCategories != null)
            {
                var categoryMapper = new CategoryMapper();
                postDTO.PostCategories = categoryMapper.MapToCategoryDtoList(postMapFrom.PostCategories);
            }

            if (postMapFrom.PostComments != null)
            {
                var commentMapper = new CommentMapper();
                var userMapper = new BlogUserMapper();
                var commentDTOList = new List<CommentDto>();
                foreach (var comment in postMapFrom.PostComments)
                {
                    var commentDTOToAdd = commentMapper.MapToCommentDto(comment);
                    if (comment.CreatedBy != null)
                    {
                        commentDTOToAdd.CreatedBy = userMapper.MapToBlogUserDto(comment.CreatedBy);
                    }
                    commentDTOList.Add(commentDTOToAdd);
                }
                postDTO.PostComments = commentDTOList;
            }

            return postDTO;
        }
    }
}