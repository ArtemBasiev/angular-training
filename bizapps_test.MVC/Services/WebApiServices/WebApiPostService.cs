using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using bizapps_test.MVC.Models;
using Newtonsoft.Json;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Util;
using Ninject.Infrastructure.Language;

namespace bizapps_test.MVC.Services.WebApiServices
{
    public class WebApiPostService : IPostService
    {
        public HttpClient PostServiceHttpClient { get; set; }

        private string ADDRESS { get; } = WebApiUrl.GetWebApiUrl()+"post/";

        public WebApiPostService()
        {
            PostServiceHttpClient = new HttpClient();
        }

        public int CreatePost(PostViewModel postViewModel, int userId, List<CategoryViewModel> categoryViewModelList)
        {
            try
            {
                postViewModel.Categories = categoryViewModelList.ToArray();
                postViewModel.UserId = userId;
                var result = PostServiceHttpClient.PostAsync(ADDRESS + "createpost", new StringContent(JsonConvert.SerializeObject(postViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result; 
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int UpdatePost(PostViewModel postViewModel, List<CategoryViewModel> categoryViewModelList)
        {
            try
            {
                postViewModel.Categories = categoryViewModelList.ToArray();
                var result = PostServiceHttpClient.PostAsync(ADDRESS + "updatepost", new StringContent(JsonConvert.SerializeObject(postViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int DeletePost(PostViewModel postViewModel)
        {
            try
            {
                var result = PostServiceHttpClient.PostAsync(ADDRESS + "deletepost", new StringContent(JsonConvert.SerializeObject(postViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<PostViewModel> GetUserPosts(int userId)
        {
            try
            {
                var result = PostServiceHttpClient.GetAsync(ADDRESS + "getuserposts/"+userId).Result;
                return result.Content.ReadAsAsync<IEnumerable<PostViewModel>>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<PostViewModel> GetUserPostsByUserName(string userName)
        {
            try
            {
                var result = PostServiceHttpClient.GetAsync(ADDRESS + "getuserpostsbyusername/" + userName).Result;
                return result.Content.ReadAsAsync<PostViewModel[]>().Result.ToEnumerable();
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<PostViewModel> GetPostsByUserNameWithoutCategory(string userName)
        {
            try
            {
                var result = PostServiceHttpClient.GetAsync(ADDRESS + "getuserpostswithoutcategory/" + userName).Result;
                return result.Content.ReadAsAsync<IEnumerable<PostViewModel>>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<PostViewModel> GetPostsByUserNameAndCategory(string userName, int categoryId)
        {
            try
            {
                var result = PostServiceHttpClient.GetAsync(ADDRESS + "getuserpostsbycategory/" + userName+"/"+categoryId).Result;
                return result.Content.ReadAsAsync<IEnumerable<PostViewModel>>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public PostViewModel GetPost(int postId)
        {
            try
            {
                var result = PostServiceHttpClient.GetAsync(ADDRESS + "getpost/" + postId).Result;
                return result.Content.ReadAsAsync<PostViewModel>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}