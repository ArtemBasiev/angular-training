using System;
using System.Collections.Generic;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using bizapps_test.MVC.Models;
using Newtonsoft.Json;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Util;

namespace bizapps_test.MVC.Services.WebApiServices
{
    public class WebApiCategoryService: ICategoryService
    {
        private HttpClient CategoryServiceHttpClient { get; }

        private string ADDRESS { get; } = WebApiUrl.GetWebApiUrl() + "category/";

        public WebApiCategoryService()
          {
            CategoryServiceHttpClient = new HttpClient();
          }


        public int CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var result = CategoryServiceHttpClient.PostAsync(ADDRESS+"createcategory", new StringContent(JsonConvert.SerializeObject(categoryViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
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
                var result = CategoryServiceHttpClient.PostAsync(ADDRESS + "updatecategory", new StringContent(JsonConvert.SerializeObject(categoryViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
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
                var result = CategoryServiceHttpClient.PostAsync(ADDRESS + "deletecategory", new StringContent(JsonConvert.SerializeObject(categoryViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
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
                var result = CategoryServiceHttpClient.GetAsync(ADDRESS + "getallcategories").Result;
                return result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>().Result;
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
                var result = CategoryServiceHttpClient.GetAsync(ADDRESS + "getpostcategories/" +postId).Result;
                return result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>().Result;
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
                var result = CategoryServiceHttpClient.GetAsync(ADDRESS + "getblogcategories/" + userName).Result;
                return result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>().Result;
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
                var result = CategoryServiceHttpClient.GetAsync(ADDRESS + "getcategorybyid/" + categoryId).Result;
                return result.Content.ReadAsAsync<CategoryViewModel>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}