using System;
using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.ASMX_Services.SoapEntities;
using bizapps_test.SL.ASMX_Services.Interfaces;
using System.Web.Services;
using Ninject;
using System.ServiceModel;
using Ninject.Web.Common.WebHost;
using System.Web;

namespace bizapps_test.SL.ASMX_Services
{
    [WebService(Namespace = "http://postservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AsmxPostService : WebService, IAsmxPostService
    {
        [Inject]
        public IPostService PostService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        public AsmxPostService()
            : this(((NinjectHttpApplication)HttpContext.Current.ApplicationInstance).Kernel.Get<IPostService>(),
                ((NinjectHttpApplication)HttpContext.Current.ApplicationInstance).Kernel.Get<ICategoryService>())
        {
        }

        public AsmxPostService(IPostService postService, ICategoryService categoryService)
        {
            PostService = postService;
            CategoryService = categoryService;
        }


        [WebMethod]
        public int CreatePost(PostSoap postSoap, int userId, CategorySoap[] categorySoapList)
        {
            try
            {
                List<CategoryDto> categoryDtoList = new List<CategoryDto>();
                foreach (CategorySoap categorySoap in categorySoapList)
                {
                    categoryDtoList.Add(new CategoryDto
                    {
                        Id = categorySoap.Id,
                        CategoryName = categorySoap.CategoryName
                    });
                }

                return PostService.CreatePost(new PostDto
                {
                    Title = postSoap.Title,
                    Body = postSoap.Body,
                    PostImage = postSoap.PostImage
                }, userId, categoryDtoList);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public int UpdatePost(PostSoap postSoap, CategorySoap[] categorySoapList)
        {
            try
            {
                List<CategoryDto> categoryDtoList = new List<CategoryDto>();
                foreach (CategorySoap categorySoap in categorySoapList)
                {
                    categoryDtoList.Add(new CategoryDto
                    {
                        Id = categorySoap.Id,
                        CategoryName = categorySoap.CategoryName
                    });
                }
                return PostService.UpdatePost(new PostDto
                {
                    Id = postSoap.Id,
                    Title = postSoap.Title,
                    Body = postSoap.Body,
                    PostImage = postSoap.PostImage
                }, categoryDtoList);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public int DeletePost(PostSoap postSoap)
        {
            try
            {
                return PostService.DeletePost(new PostDto
                {
                    Id = postSoap.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public PostSoap[] GetUserPosts(int userId)
        {
            try
            {
                List<PostSoap> postSoapList = new List<PostSoap>();
                IEnumerable<PostDto> postDtoList = PostService.GetUserPosts(userId);
                foreach (PostDto postDto in postDtoList)
                {
                    postSoapList.Add(new PostSoap
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public PostSoap[] GetUserPostsByUserName(string userName)
        {
            try
            {
                List<PostSoap> postSoapList = new List<PostSoap>();
                IEnumerable<PostDto> postDtoList = PostService.GetUserPostsByUserName(userName);
                foreach (PostDto postDto in postDtoList)
                {
                    postSoapList.Add(new PostSoap
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public PostSoap[] GetPostsByUserNameWithoutCategory(string userName)
        {
            try
            {
                List<PostSoap> postSoapList = new List<PostSoap>();
                IEnumerable<PostDto> postDtoList = PostService.GetPostsByUserNameWithoutCategory(userName);
                foreach (PostDto postDto in postDtoList)
                {
                    postSoapList.Add(new PostSoap
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public PostSoap[] GetPostsByUserNameAndCategory(string userName, int categoryId)
        {
            try
            {
                List<PostSoap> postSoapList = new List<PostSoap>();
                IEnumerable<PostDto> postDtoList = PostService.GetPostsByUserNameAndCategory(userName, categoryId);
                foreach (PostDto postDto in postDtoList)
                {
                    postSoapList.Add(new PostSoap
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Body = postDto.Body,
                        CreationDate = postDto.CreationDate,
                        PostImage = postDto.PostImage
                    });
                }
                return postSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public PostSoap GetPost(int postId)
        {
            try
            {
                PostDto postDto = PostService.GetPost(postId);
                return new PostSoap
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
