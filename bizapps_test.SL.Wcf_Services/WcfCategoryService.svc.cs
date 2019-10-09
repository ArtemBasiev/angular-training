using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.Wcf_Services.DataContracts;
using Ninject;

namespace bizapps_test.SL.Wcf_Services
{
    public class WcfCategoryService : IWcfCategoryService
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        public int CreateCategory(CategoryDC categoryDC)
        {
            try
            {
                return CategoryService.CreateCategory(new CategoryDto
                {
                    CategoryName = categoryDC.CategoryName
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int UpdateCategory(CategoryDC categoryDC)
        {
            try
            {
                return CategoryService.UpdateCategory(new CategoryDto
                {
                    Id = categoryDC.Id,
                    CategoryName = categoryDC.CategoryName
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int DeleteCategory(CategoryDC categoryDC)
        {
            try
            {
                return CategoryService.DeleteCategory(new CategoryDto
                {
                    Id = categoryDC.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<CategoryDC> GetAllCategories()
        {
            try
            {
                List<CategoryDC> categoryDCList = new List<CategoryDC>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetAllCategories();
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categoryDCList.Add(new CategoryDC
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categoryDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<CategoryDC> GetPostCategories(int postId)
        {
            try
            {
                List<CategoryDC> categoryDCList = new List<CategoryDC>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetPostCategories(postId);
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categoryDCList.Add(new CategoryDC
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categoryDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<CategoryDC> GetBlogCategories(string userName)
        {
            try
            {
                List<CategoryDC> categoryDCList = new List<CategoryDC>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetBlogCategories(userName);
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categoryDCList.Add(new CategoryDC
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categoryDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public CategoryDC GetCategoryById(int categoryId)
        {
            try
            {
                CategoryDto categoryDto = CategoryService.GetCategoryById(categoryId);
                return new CategoryDC
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName
                };
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
