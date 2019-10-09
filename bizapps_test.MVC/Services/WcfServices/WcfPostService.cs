using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.WcfPostServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.WcfServices
{
    public class WcfPostService: IPostService
    {
        public WcfPostServiceClient PostServiceClient { get; set; }

        public WcfPostService()
        {
            PostServiceClient = new WcfPostServiceClient();
        }

        public int CreatePost(PostViewModel postViewModel, int userId, List<CategoryViewModel> categoryViewModelList)
        {
            try
            {
                List<CategoryDC> categoryDCList = new List<CategoryDC>();
                foreach (CategoryViewModel categoryViewModel in categoryViewModelList)
                {
                    categoryDCList.Add(new CategoryDC
                    {
                        Id = categoryViewModel.Id,
                        CategoryName = categoryViewModel.CategoryName
                    });
                }

                return PostServiceClient.CreatePost(new PostDC
                {
                    Title = postViewModel.Title,
                    Body = postViewModel.Body,
                    PostImage = postViewModel.PostImage
                }, userId, categoryDCList.ToArray());
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
                List<CategoryDC> categoryDCList = new List<CategoryDC>();
                foreach (CategoryViewModel categoryViewModel in categoryViewModelList)
                {
                    categoryDCList.Add(new CategoryDC
                    {
                        Id = categoryViewModel.Id,
                        CategoryName = categoryViewModel.CategoryName
                    });
                }
                return PostServiceClient.UpdatePost(new PostDC
                {
                    Id = postViewModel.Id,
                    Title = postViewModel.Title,
                    Body = postViewModel.Body,
                    PostImage = postViewModel.PostImage
                }, categoryDCList.ToArray());
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
                return PostServiceClient.DeletePost(new PostDC
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
                IEnumerable<PostDC> postDCList = PostServiceClient.GetUserPosts(userId);
                foreach (PostDC postDC in postDCList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = postDC.Id,
                        Title = postDC.Title,
                        Body = postDC.Body,
                        CreationDate = postDC.CreationDate.ToString(),
                        PostImage = postDC.PostImage
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
                IEnumerable<PostDC> postDCList = PostServiceClient.GetUserPostsByUserName(userName);
                foreach (PostDC postDC in postDCList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = postDC.Id,
                        Title = postDC.Title,
                        Body = postDC.Body,
                        CreationDate = postDC.CreationDate.ToString(),
                        PostImage = postDC.PostImage
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
                IEnumerable<PostDC> postDCList = PostServiceClient.GetPostsByUserNameWithoutCategory(userName);
                foreach (PostDC postDC in postDCList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = postDC.Id,
                        Title = postDC.Title,
                        Body = postDC.Body,
                        CreationDate = postDC.CreationDate.ToString(),
                        PostImage = postDC.PostImage
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
                IEnumerable<PostDC> postDCList = PostServiceClient.GetPostsByUserNameAndCategory(userName, categoryId);
                foreach (PostDC postDC in postDCList)
                {
                    postViewModelList.Add(new PostViewModel
                    {
                        Id = postDC.Id,
                        Title = postDC.Title,
                        Body = postDC.Body,
                        CreationDate = postDC.CreationDate.ToString(),
                        PostImage = postDC.PostImage
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
                PostDC postDC = PostServiceClient.GetPost(postId);
                return new PostViewModel
                {
                    Id = postDC.Id,
                    Title = postDC.Title,
                    Body = postDC.Body,
                    CreationDate = postDC.CreationDate.ToString(),
                    PostImage = postDC.PostImage
                };
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}