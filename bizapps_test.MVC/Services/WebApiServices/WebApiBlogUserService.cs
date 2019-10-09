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
    public class WebApiBlogUserService: IBlogUserService
    {
        private HttpClient BlogUserServiceHttpClient { get; }

        private string ADDRESS { get; } = WebApiUrl.GetWebApiUrl() + "bloguser/";

        public WebApiBlogUserService()
        {
            BlogUserServiceHttpClient = new HttpClient();
        }

        public int CreateBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                var result = BlogUserServiceHttpClient.PostAsync(ADDRESS + "createbloguser", new StringContent(JsonConvert.SerializeObject(bloguserViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public int UpdateBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                var result = BlogUserServiceHttpClient.PostAsync(ADDRESS + "updatebloguser", new StringContent(JsonConvert.SerializeObject(bloguserViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public int DeleteBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                var result = BlogUserServiceHttpClient.PostAsync(ADDRESS + "deletebloguser", new StringContent(JsonConvert.SerializeObject(bloguserViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public int ChangePassword(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                var result = BlogUserServiceHttpClient.PostAsync(ADDRESS + "changepassword", new StringContent(JsonConvert.SerializeObject(bloguserViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public IEnumerable<BlogUserViewModel> GetAllUsers()
        {
            try
            {
                var result = BlogUserServiceHttpClient.GetAsync(ADDRESS + "getallusers").Result;
                return result.Content.ReadAsAsync<IEnumerable<BlogUserViewModel>>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public BlogUserViewModel GetBlogUserById(int userId)
        {
            try
            {
                var result = BlogUserServiceHttpClient.GetAsync(ADDRESS + "getbloguserbyid/"+userId).Result;
                return result.Content.ReadAsAsync<BlogUserViewModel>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public BlogUserViewModel GetBlogUserByName(string userName)
        {
            try
            {
                var result = BlogUserServiceHttpClient.GetAsync(ADDRESS + "getbloguserbyname/" + userName).Result;
                return result.Content.ReadAsAsync<BlogUserViewModel>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public BlogUserViewModel GetBlogUserNameAndPassword(BlogUserViewModel incomingUser)
        {
            try
            {
                var result = BlogUserServiceHttpClient.PostAsync(ADDRESS + "checkusernameandpassword", new StringContent(JsonConvert.SerializeObject(incomingUser), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<BlogUserViewModel>().Result;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public int GetAdminPermission(string userName)
        {
            try
            {
                
                var result = BlogUserServiceHttpClient.GetAsync(ADDRESS + "getadminpermission/" + userName).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}