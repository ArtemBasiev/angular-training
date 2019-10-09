using System;
using System.Web.Http;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Attributes;
using Newtonsoft.Json.Linq;
using Ninject;

namespace bizapps_test.SL.API_Services.Controllers
{
    [RoutePrefix("blog")]
    public class BlogController : ApiController
    {
        [Inject]
        public IBlogService BlogService { get; set; }


        [Route("createblog")]
        [CustomAuthorize]
        [HttpPost]
        public bool CreateBlog(JObject blogDto)
        {
            try
            {
                var blog = new BlogDto
                {
                    BlogTitle = blogDto["BlogTitle"].ToString(),
                    CreatedBy = blogDto["CreatedBy"].ToObject<BlogUserDto>(),
                    CreationDate = DateTime.Now
                };
                var result = BlogService.CreateBlog(blog);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            

        }

        [Route("updateblog")]
        [CustomAuthorize]
        [HttpPost]
        public bool UpdateBlog([FromBody]BlogDto blogDto)
        {
            try
            {
                blogDto.CreationDate = DateTime.Now;
                var result = BlogService.UpdateBlog(blogDto);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            

        }

        [Route("deleteblog")]
        [CustomAuthorize]
        [HttpPost]
        public bool DeleteBlog([FromBody]BlogDto blogDto)
        {
            try
            {
                var result = BlogService.DeleteBlog(blogDto);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            

        }

        [Route("getblogbyid/{blogId}")]
        [HttpGet]
        public BlogDto GetBlogById(int blogId)
        {
            try
            {
                var result = BlogService.GetBlogById(blogId);
                if (result.Status == AnswerStatus.Successfull)
                {
                    return result.ReceivedEntity;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [Route("getblogbyuserid/{userId}")]
        [HttpGet]
        public BlogDto GetBlogUserById(int userId)
        {
            try
            {
                var result = BlogService.GetBlogByUserId(userId);
                if (result.Status == AnswerStatus.Successfull)
                {
                    if (result.ReceivedEntity.Id != 0)
                    {
                        return result.ReceivedEntity;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
        }

    }
}
