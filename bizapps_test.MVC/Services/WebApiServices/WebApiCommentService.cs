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
    public class WebApiCommentService: ICommentService
    {
        private HttpClient CommentServiceHttpClient { get; }

        private string ADDRESS { get; } = WebApiUrl.GetWebApiUrl() + "comment/";

        public WebApiCommentService()
        {
            CommentServiceHttpClient = new HttpClient();
        }

        public int CreateComment(CommentViewModel commentViewModel, int postId)
        {
            try
            {
                var result = CommentServiceHttpClient.PostAsync(ADDRESS + "createcomment/"+postId, new StringContent(JsonConvert.SerializeObject(commentViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int UpdateComment(CommentViewModel commentViewModel)
        {
            try
            {
                var result = CommentServiceHttpClient.PostAsync(ADDRESS + "updatecomment", new StringContent(JsonConvert.SerializeObject(commentViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int DeleteComment(CommentViewModel commentViewModel)
        {
            try
            {
                var result = CommentServiceHttpClient.PostAsync(ADDRESS + "deletecomment", new StringContent(JsonConvert.SerializeObject(commentViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<int>().Result;

            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CommentViewModel> GetIndependentComments(int postId)
        {
            try
            {
                var result = CommentServiceHttpClient.GetAsync(ADDRESS + "getindependentcomments/"+postId).Result;
                return result.Content.ReadAsAsync<IEnumerable<CommentViewModel>>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CommentViewModel> GetCommentAnswers(int commentId)
        {
            try
            {
                var result = CommentServiceHttpClient.GetAsync(ADDRESS + "getcommentanswers/" + commentId).Result;
                return result.Content.ReadAsAsync<IEnumerable<CommentViewModel>>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public CommentViewModel GetComment(int commentId)
        {
            try
            {
                var result = CommentServiceHttpClient.GetAsync(ADDRESS + "getcomment/" + commentId).Result;
                return result.Content.ReadAsAsync<CommentViewModel>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public bool HaveMoreThanFourLevels(CommentViewModel commentViewModel)
        {
            try
            {
                var result = CommentServiceHttpClient.PostAsync(ADDRESS + "havemorethanfourlevels", new StringContent(JsonConvert.SerializeObject(commentViewModel), Encoding.UTF8, "application/json")).Result;
                return result.Content.ReadAsAsync<bool>().Result;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}