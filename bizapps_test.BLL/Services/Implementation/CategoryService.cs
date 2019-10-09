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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IRepositoryFactory _repositoryFactory;

        private readonly ICustomLogger _logger;

        private readonly ICategoryBuilder _categoryBuilder;

        private readonly IBlogBuilder _blogBuilder;

        public CategoryService(IUnitOfWorkFactory uowFactory, IRepositoryFactory repositoryFactory, ILogFactory logFactory)
        {
            _unitOfWorkFactory = uowFactory;
            _repositoryFactory = repositoryFactory;
            _categoryBuilder = new CategoryBuilder();
            _blogBuilder = new BlogBuilder();
            _logger = logFactory.CreateLogger(GetType());
        }


        public AnswerStatus CreateCategory(CategoryDto categoryDTO)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {
                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);

                    var result = CreateCategory(categoryDTO, categoryRepository);

                    if (result == AnswerStatus.Failed)
                    {
                        return result;
                    }
                    uow.SaveChanges();
                    return result;
                }
            }
            catch(Exception exc)
            {
                _logger.Log(exc.ToString());

                return AnswerStatus.Failed;
            }
            
        }


        public AnswerStatus CreateCategory(CategoryDto categoryDTO, ICategoryRepository categoryRepository)
        {
                try
                {
                    var blogRepository = _repositoryFactory.CreateBlogRepository(categoryRepository.UnitOfWork);
                    var categoryToCreate = _categoryBuilder.CreateCategory(categoryDTO.CategoryName);
                    var categoryBlog = blogRepository.GetEntityById(categoryDTO.RelatedTo.Id);
                    _blogBuilder.SetBlogId(categoryBlog, categoryDTO.RelatedTo.Id);
                    _categoryBuilder.SetBlogRelatedTo(categoryToCreate, categoryBlog);

                    categoryRepository.CreateEntity(categoryToCreate);

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }
            
        }


        public AnswerStatus UpdateCategory(CategoryDto categoryDTO)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var categoryToUpdate = _categoryBuilder.CreateCategory(categoryDTO.CategoryName);
                    _categoryBuilder.SetCategoryId(categoryToUpdate, categoryDTO.Id);

                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);
                    categoryRepository.UpdateEntity(categoryToUpdate);

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


        public AnswerStatus DeleteCategory(CategoryDto categoryDTO)
        {
            try
            {
                using (var uow = _unitOfWorkFactory.Create())
                {
                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);
                    var result = DeleteCategory(categoryDTO, categoryRepository);
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


        public AnswerStatus DeleteCategory(CategoryDto categoryDTO, ICategoryRepository categoryRepository)
        {
                try
                {
                    if (categoryDTO.Id <= 0)
                    {
                        throw new ArgumentException("id can't be less or equal to 0");
                    }

                    var categoryToDelete = categoryRepository.GetEntityById(categoryDTO.Id);

                    RemoveCategoryFromPosts(categoryToDelete, categoryRepository.UnitOfWork);

                    categoryRepository.DeleteEntity(categoryToDelete);

                    return AnswerStatus.Successfull;
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return AnswerStatus.Failed;
                }           
        }


        private void RemoveCategoryFromPosts(Category categoryToRemove, IUnitOfWork uow)
        {
            var postRepository = _repositoryFactory.CreatePostRepository(uow);

            var posts = postRepository.GetPostsByCategoryId(categoryToRemove.Id);
            foreach (var post in posts)
            {
                postRepository.RemoveCategoryFromPost(post, categoryToRemove);
            }
        }


        public ServiceAnswer<IEnumerable<CategoryDto>> GetPostCategories(int postId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);

                    var receivedCategories = categoryRepository.GetPostCategories(postId);

                    var categoryDTOList = new List<CategoryDto>();

                    if (receivedCategories != null)
                    {
                        var mapper = new CategoryMapper();
                        categoryDTOList = mapper.MapToCategoryDtoList(receivedCategories).ToList();
                    }

                    return new ServiceAnswer<IEnumerable<CategoryDto>>(categoryDTOList, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<IEnumerable<CategoryDto>>(null, AnswerStatus.Failed);
                }
            }        
        }


        public ServiceAnswer<IEnumerable<CategoryDto>> GetBlogCategories(int blogId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);

                    var receivedCategories = categoryRepository.GetBlogCategories(blogId);

                    var categoryDTOList = new List<CategoryDto>();

                    if (receivedCategories != null)
                    {
                        var mapper = new CategoryMapper();
                        categoryDTOList = mapper.MapToCategoryDtoList(receivedCategories).ToList();
                    }

                    return new ServiceAnswer<IEnumerable<CategoryDto>>(categoryDTOList, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<IEnumerable<CategoryDto>>(null, AnswerStatus.Failed);
                }
            }          
        }


        public ServiceAnswer<CategoryDto> GetCategoryById(int categoryId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var categoryRepository = _repositoryFactory.CreateCategoryRepository(uow);

                    var receivedCategory = categoryRepository.GetEntityById(categoryId);

                    var categoryDTO = new CategoryDto();

                    if (receivedCategory != null)
                    {
                        var mapper = new CategoryMapper();
                        categoryDTO = mapper.MapToCategoryDto(receivedCategory);
                    }                        
                    
                    return new ServiceAnswer<CategoryDto>(categoryDTO, AnswerStatus.Successfull);
                }
                catch (Exception exc)
                {
                    _logger.Log(exc.ToString());

                    return new ServiceAnswer<CategoryDto>(null, AnswerStatus.Failed);
                }
            }          
        }
    }
}