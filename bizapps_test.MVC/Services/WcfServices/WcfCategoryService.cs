using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.WcfCategoryServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;


namespace bizapps_test.MVC.Services.WcfServices
{
    public class WcfCategoryService: ICategoryService
    {
        public WcfCategoryServiceClient CategoryServiceClient { get; set; }

        public WcfCategoryService()
        {
            CategoryServiceClient = new WcfCategoryServiceClient();
        }

        public int CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                
                return CategoryServiceClient.CreateCategory(new CategoryDC
                {
                    CategoryName = categoryViewModel.CategoryName
                });
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                return CategoryServiceClient.UpdateCategory(new CategoryDC
                {
                    Id = categoryViewModel.Id,
                    CategoryName = categoryViewModel.CategoryName
                });
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int DeleteCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                return CategoryServiceClient.DeleteCategory(new CategoryDC
                {
                    Id = categoryViewModel.Id
                });
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            try
            {
                List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
                IEnumerable<CategoryDC> categoryDCList = CategoryServiceClient.GetAllCategories();
                foreach (CategoryDC categoryDC in categoryDCList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categoryDC.Id,
                        CategoryName = categoryDC.CategoryName
                    });
                }
                return categoryViewModelList;
            }
            catch (FaultException xe)
            {
                throw new ApplicationException(xe.Message);
            }
        }


        public IEnumerable<CategoryViewModel> GetPostCategories(int postId)
        {
            try
            {
                List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
                IEnumerable<CategoryDC> categoryDCList = CategoryServiceClient.GetPostCategories(postId);
                foreach (CategoryDC categoryDC in categoryDCList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categoryDC.Id,
                        CategoryName = categoryDC.CategoryName
                    });
                }
                return categoryViewModelList;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CategoryViewModel> GetBlogCategories(string userName)
        {
            try
            {
                List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
                IEnumerable<CategoryDC> categoryDCList = CategoryServiceClient.GetBlogCategories(userName);
                foreach (CategoryDC categoryDC in categoryDCList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categoryDC.Id,
                        CategoryName = categoryDC.CategoryName
                    });
                }
                return categoryViewModelList;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public CategoryViewModel GetCategoryById(int categoryId)
        {
            try
            {
                CategoryDC categoryDC = CategoryServiceClient.GetCategoryById(categoryId);
                return new CategoryViewModel
                {
                    Id = categoryDC.Id,
                    CategoryName = categoryDC.CategoryName
                };
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}