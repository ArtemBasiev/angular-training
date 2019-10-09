using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.AsmxCategoryServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.AsmxServices
{
    public class AsmxCategoryService : ICategoryService
    {
        public AsmxCategoryServiceSoapClient CategoryServiceClient { get; set; }

        public AsmxCategoryService()
        {
            CategoryServiceClient = new AsmxCategoryServiceSoapClient();
        }

        public int CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {

                return CategoryServiceClient.CreateCategory(new CategorySoap
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
                return CategoryServiceClient.UpdateCategory(new CategorySoap
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
                return CategoryServiceClient.DeleteCategory(new CategorySoap
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
                IEnumerable<CategorySoap> categorySoapList = CategoryServiceClient.GetAllCategories();
                foreach (CategorySoap categorySoap in categorySoapList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categorySoap.Id,
                        CategoryName = categorySoap.CategoryName
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
                IEnumerable<CategorySoap> categorySoapList = CategoryServiceClient.GetPostCategories(postId);
                foreach (CategorySoap categorySoap in categorySoapList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categorySoap.Id,
                        CategoryName = categorySoap.CategoryName
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
                IEnumerable<CategorySoap> categorySoapList = CategoryServiceClient.GetBlogCategories(userName);
                foreach (CategorySoap categorySoap in categorySoapList)
                {
                    categoryViewModelList.Add(new CategoryViewModel
                    {
                        Id = categorySoap.Id,
                        CategoryName = categorySoap.CategoryName
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
                CategorySoap categorySoap = CategoryServiceClient.GetCategoryById(categoryId);
                return new CategoryViewModel
                {
                    Id = categorySoap.Id,
                    CategoryName = categorySoap.CategoryName
                };
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}