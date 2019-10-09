using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.AsmxPostServiceReference;

using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.AsmxServices
{
    public class AsmxPostService: IPostService
    {
        private AsmxPostServiceSoapClient PostServiceClient { get; }


        public AsmxPostService()
        {
            PostServiceClient = new AsmxPostServiceSoapClient();
        }


        public int CreatePost(PostViewModel postViewModel, int userId, List<CategoryViewModel> categoryViewModelList)
        {
            try
            {
                List<CategorySoap> categorySoapList = new List<CategorySoap>();
                foreach (CategoryViewModel categoryViewModel in categoryViewModelList)
                {
                    categorySoapList.Add(new CategorySoap
                    {
                        Id = categoryViewModel.Id,
                        CategoryName = categoryViewModel.CategoryName
                    });
                }

                return PostServiceClient.CreatePost(new PostSoap
                {
                    Title = postViewModel.Title,
                    Body = postViewModel.Body,
                    PostImage = postViewModel.PostImage
                }, userId, categorySoapList.ToArray());
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
                List<CategorySoap> categorySoapList = new List<CategorySoap>();
                foreach (CategoryViewModel categoryViewModel in categoryViewModelList)
                {
                    categorySoapList.Add(new CategorySoap
                    {
                        Id = categoryViewModel.Id,
                        CategoryName = categoryViewModel.CategoryName
                    });
                }
                return PostServiceClient.UpdatePost(new PostSoap
                {
                    Id = postViewModel.Id,
                    Title = postViewModel.Title,
                    Body = postViewModel.Body,
                    PostImage = postViewModel.PostImage
                }, categorySoapList.ToArray());
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
                return PostServiceClient.DeletePost(new PostSoap
                {
                    Id = postViewModel.Id
                });
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
                List<PostViewModel> postViewModelList = new List<PostViewModel>();
                IEnumerable<PostSoap> postSoapList = PostServiceClient.GetUserPosts(userId);
                foreach (PostSoap post in postSoapList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreationDate = post.CreationDate.ToString(),
                        PostImage = post.PostImage
                    });
                }
                return postViewModelList;
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
                List<PostViewModel> postViewModelList = new List<PostViewModel>();
                IEnumerable<PostSoap> postSoapList = PostServiceClient.GetUserPostsByUserName(userName);
                foreach (PostSoap post in postSoapList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreationDate = post.CreationDate.ToString(),
                        PostImage = post.PostImage
                    });
                }
                return postViewModelList;
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
                List<PostViewModel> postViewModelList = new List<PostViewModel>();
                IEnumerable<PostSoap> postSoapList = PostServiceClient.GetPostsByUserNameWithoutCategory(userName);
                foreach (PostSoap post in postSoapList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreationDate = post.CreationDate.ToString(),
                        PostImage = post.PostImage
                    });
                }
                return postViewModelList;
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
                List<PostViewModel> postViewModelList = new List<PostViewModel>();
                IEnumerable<PostSoap> postSoapList = PostServiceClient.GetPostsByUserNameAndCategory(userName, categoryId);
                foreach (PostSoap post in postSoapList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreationDate = post.CreationDate.ToString(),
                        PostImage = post.PostImage
                    });
                }
                return postViewModelList;
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
                PostSoap post = PostServiceClient.GetPost(postId);
                return new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CreationDate = post.CreationDate.ToString(),
                    PostImage = post.PostImage
                };
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}