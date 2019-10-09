using System;
using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Mappers;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Services.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly BlogBuilder _blogBuilder;
        private readonly BlogUserBuilder _userBuilder;
        private readonly ICustomLogger _logger;
      

        public BlogService(ICategoryService categoryService, IPostService postService, IUnitOfWorkFactory uowFactory, 
            IRepositoryFactory repositoryFactory, ILogFactory logFactory)
        {
            _blogBuilder = new BlogBuilder();
            _userBuilder = new BlogUserBuilder();
            _categoryService = categoryService;
            _postService = postService;
            _unitOfWorkFactory = uowFactory;
            _repositoryFactory = repositoryFactory;
            _logger = logFactory.CreateLogger(GetType());
        }

        public AnswerStatus CreateBlog(BlogDto blogDTO)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {
                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);

                    var result = CreateBlog(blogDTO, blogRepository);

                    if (result == AnswerStatus.Failed)
                    {
                        return result;
                    }

                    uow.SaveChanges();
                    return result;
                }
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return AnswerStatus.Failed;
            }                      
        }



        public AnswerStatus CreateBlog(BlogDto blogDTO, IBlogRepository blogRepository)
        {
                try
                {
                    var newBlog = _blogBuilder.CreateBlog(blogDTO.BlogTitle, DateTime.Now);
                    var userRepository = _repositoryFactory.CreateUserRepository(blogRepository.UnitOfWork);
                    var userCreatedBy = userRepository.GetEntityById(blogDTO.CreatedBy.Id);
                    _userBuilder.SetBlogUserId(userCreatedBy, blogDTO.CreatedBy.Id);
                    _blogBuilder.SetBlogUser(newBlog, userCreatedBy);

                    blogRepository.CreateEntity(newBlog);
                    blogDTO.Id = newBlog.Id;
                    blogDTO.CreationDate = newBlog.CreationDate;

                if (blogDTO.BlogCategories != null)
                    {
                        var result =  CreateCategories(blogDTO, blogRepository.UnitOfWork);
                        if (result == AnswerStatus.Failed)
                        {
                            return result;
                        }
                    }

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }

        }

        private AnswerStatus CreateCategories(BlogDto blogDTO ,IUnitOfWork uow)
        {
            var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);


            foreach (var categoryToAdd in blogDTO.BlogCategories)
            {
                categoryToAdd.RelatedTo = blogDTO;
                AnswerStatus status = _categoryService.CreateCategory(categoryToAdd, categoryRepository);
                if (status == AnswerStatus.Failed)
                {
                    return AnswerStatus.Failed;
                }
            }
            return AnswerStatus.Successfull;
        }


        public AnswerStatus UpdateBlog(BlogDto blogDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var blogToUpdate = _blogBuilder.CreateBlog(blogDTO.BlogTitle, blogDTO.CreationDate);
                    _blogBuilder.SetBlogId(blogToUpdate, blogDTO.Id);

                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);
                    blogRepository.UpdateEntity(blogToUpdate);
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


        public AnswerStatus DeleteBlog(BlogDto blogDTO)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {
                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);
                    var result = DeleteBlog(blogDTO, blogRepository);

                    if (result == AnswerStatus.Failed)
                    {
                        return result;
                    }

                    uow.SaveChanges();

                    return AnswerStatus.Successfull;
                }
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return AnswerStatus.Failed;
            }       
        }


        public AnswerStatus DeleteBlog(BlogDto blogDTO, IBlogRepository blogRepository)
        {
            try
            {
                if (blogDTO.Id <= 0)
                {
                    throw new ArgumentException("id can't be less or equal to 0");
                }

                var blogToDelete = blogRepository.GetEntityById(blogDTO.Id);

                var deletePostsResult = DeletePosts(blogDTO.Id, blogRepository.UnitOfWork);
                if (deletePostsResult == AnswerStatus.Failed)
                {
                    return deletePostsResult;
                }

                var deleteCategoriesResult = DeleteCategories(blogDTO.Id, blogRepository.UnitOfWork);
                if (deleteCategoriesResult == AnswerStatus.Failed)
                {
                    return deleteCategoriesResult;
                }


                blogRepository.DeleteEntity(blogToDelete);

                return AnswerStatus.Successfull;
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return AnswerStatus.Failed;
            }

        }


        private AnswerStatus DeleteCategories(int blogId, IUnitOfWork uow)
        {
            var categoryServiceAnswer = _categoryService.GetBlogCategories(blogId);
            if (categoryServiceAnswer.Status == AnswerStatus.Failed)
            {
                return AnswerStatus.Failed;
            }

            var blogCategories = categoryServiceAnswer.ReceivedEntity;
            var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);
            foreach (var categoryDTO in blogCategories)
            {
               AnswerStatus status = _categoryService.DeleteCategory(categoryDTO, categoryRepository);
               if (status == AnswerStatus.Failed)
               {
                  return AnswerStatus.Failed;
               }
            }

            return AnswerStatus.Successfull;
        }


        private AnswerStatus DeletePosts(int blogId, IUnitOfWork uow)
        {
            var postServiceAnswer = _postService.GetBlogPosts(blogId);
            if (postServiceAnswer.Status == AnswerStatus.Failed)
            {
                return AnswerStatus.Failed;
            }
            
            var blogPosts = postServiceAnswer.ReceivedEntity;
            foreach (var postDTO in blogPosts)
            {
               var postRepository = _repositoryFactory.CreatePostRepository(uow);

               AnswerStatus status = _postService.DeletePost(postDTO, postRepository);
               if (status == AnswerStatus.Failed)
               {                  
                  return AnswerStatus.Failed;
               }
            }

            return AnswerStatus.Successfull;
        }



        public ServiceAnswer<BlogDto> GetBlogById(int blogId)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {

                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);

                    var receivedBlog = blogRepository.GetEntityById(blogId);

                    var blogDTO = new BlogDto();

                    if (receivedBlog != null)
                    {
                        blogDTO = MapReceivedBlog(receivedBlog);
                    }

                    return new ServiceAnswer<BlogDto>(blogDTO, AnswerStatus.Successfull);
                }
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return new ServiceAnswer<BlogDto>(null, AnswerStatus.Failed);
            }
        }


        public ServiceAnswer<BlogDto> GetBlogByUserId(int userId)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {

                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);

                    var receivedBlog = blogRepository.GetBlogByUserId(userId);

                    var blogDTO = new BlogDto();

                    if (receivedBlog != null)
                    {
                        blogDTO = MapReceivedBlog(receivedBlog);
                    }

                    return new ServiceAnswer<BlogDto>(blogDTO, AnswerStatus.Successfull);

                }
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return new ServiceAnswer<BlogDto>(null, AnswerStatus.Failed);
            }

        }

        public ServiceAnswer<IEnumerable<BlogDto>> GetAllBlogs()
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {

                    var blogRepository = _repositoryFactory.CreateBlogRepository(uow);

                    var receivedBlogs = blogRepository.GetAllBlogs();

                    var blogDTOList = new List<BlogDto>();

                    foreach (var receivedBlog in receivedBlogs) 
                    {
                        blogDTOList.Add(MapReceivedBlog(receivedBlog)); 
                    }

                    return new ServiceAnswer<IEnumerable<BlogDto>>(blogDTOList, AnswerStatus.Successfull);

                }
            }
            catch (Exception exc)
            {
                _logger.Log(exc.ToString());

                return new ServiceAnswer<IEnumerable<BlogDto>>(null, AnswerStatus.Failed);
            }

        }


        private BlogDto MapReceivedBlog(Blog blogMapFrom)
        {
            var blogMapper = new BlogMapper();
            var categoryMapper = new CategoryMapper();           
            
            BlogDto blogDTO = blogMapper.MapToBlogDto(blogMapFrom);

            if (blogMapFrom.BlogCategories != null)
            {
                
                blogDTO.BlogCategories = categoryMapper.MapToCategoryDtoList(blogMapFrom.BlogCategories);
            }

            if (blogMapFrom.BlogPosts != null)
            {
                var postMapper = new PostMapper();
                var postDTOList = new List<PostDto>(); 
                foreach (var post in blogMapFrom.BlogPosts)
                {
                    var postDTOToAdd = postMapper.MapToPostDto(post);
                    if (post.PostCategories != null)
                    {
                        postDTOToAdd.PostCategories = categoryMapper.MapToCategoryDtoList(post.PostCategories);
                    }
                    postDTOList.Add(postDTOToAdd);
                }

                blogDTO.BlogPosts = postDTOList;
            }

            if (blogMapFrom.CreatedBy != null)
            {
                var userMapper = new BlogUserMapper();
                blogDTO.CreatedBy = userMapper.MapToBlogUserDto(blogMapFrom.CreatedBy);
            }

            return blogDTO;
        }
    }
}