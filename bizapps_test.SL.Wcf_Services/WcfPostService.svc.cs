using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.Wcf_Services.DataContracts;
using Ninject;

namespace bizapps_test.SL.Wcf_Services
{
    public class WcfPostService : IWcfPostService
    {
        [Inject]
        public IPostService PostService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        public int CreatePost(PostDC postDC, int userId, List<CategoryDC> categoryListDC)
        {
            try
            {
                List<CategoryDto> categoryDtoList = new List<CategoryDto>();
                foreach (CategoryDC categoryDC in categoryListDC)
                {
                    categoryDtoList.Add(new CategoryDto
                    {
                        Id = categoryDC.Id,
                        CategoryName = categoryDC.CategoryName
                    });
                }

                return PostService.CreatePost(new PostDto
                {
                    Title = postDC.Title,
                    Body = postDC.Body,
                    PostImage = postDC.PostImage
                }, userId, categoryDtoList);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int UpdatePost(PostDC postDC, List<CategoryDC> categoryListDC)
        {
            try
            {
                List<CategoryDto> categoryDtoList = new List<CategoryDto>();
                foreach (CategoryDC categoryDC in categoryListDC)
                {
                    categoryDtoList.Add(new CategoryDto
                    {
                        Id = categoryDC.Id,
                        CategoryName = categoryDC.CategoryName
                    });
                }
                return PostService.UpdatePost(new PostDto
                {
                    Id = postDC.Id,
                    Title = postDC.Title,
                    Body = postDC.Body,
                    PostImage = postDC.PostImage
                }, categoryDtoList);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int DeletePost(PostDC postDC)
        {
            try
            {
                return PostService.DeletePost(new PostDto
                {
                    Id = postDC.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<PostDC> GetUserPosts(int userId)
        {
            try
            {
                List<PostDC> postDCList = new List<PostDC>();
                IEnumerable<PostDto> postDtoList = PostService.GetUserPosts(userId);
                foreach (PostDto postDto in postDtoList)
                {
                    postDCList.Add(new PostDC
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage                      
                    });
                }
                return postDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<PostDC> GetUserPostsByUserName(string userName)
        {
            try
            {
                List<PostDC> postDCList = new List<PostDC>();
                IEnumerable<PostDto> postDtoList = PostService.GetUserPostsByUserName(userName);
                foreach (PostDto postDto in postDtoList)
                {
                    postDCList.Add(new PostDC
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<PostDC> GetPostsByUserNameWithoutCategory(string userName)
        {
            try
            {
                List<PostDC> postDCList = new List<PostDC>();
                IEnumerable<PostDto> postDtoList = PostService.GetPostsByUserNameWithoutCategory(userName);
                foreach (PostDto postDto in postDtoList)
                {
                    postDCList.Add(new PostDC
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<PostDC> GetPostsByUserNameAndCategory(string userName, int categoryId)
        {
            try
            {
                List<PostDC> postDCList = new List<PostDC>();
                IEnumerable<PostDto> postDtoList = PostService.GetPostsByUserNameAndCategory(userName, categoryId);
                foreach (PostDto postDto in postDtoList)
                {
                    postDCList.Add(new PostDC
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public PostDC GetPost(int postId)
        {
            try
            {
                PostDto postDto = PostService.GetPost(postId);
                return new PostDC
                {
                    Id = postDto.Id,
                    Title = postDto.Title,
                    Body = postDto.Body,
                    CreationDate = postDto.CreationDate,
                    PostImage = postDto.PostImage
                };
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
