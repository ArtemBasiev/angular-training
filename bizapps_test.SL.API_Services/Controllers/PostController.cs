using System;
using System.Collections.Generic;
using System.Web.Http;
using bizapps_test.BLL.DTO;
using Ninject;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Attributes;
using Newtonsoft.Json.Linq;

namespace bizapps_test.SL.API_Services.Controllers
{
    [RoutePrefix("post")]
    public class PostController : ApiController
    {
        [Inject]
        public IPostService PostService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }


        [Route("createpost")]
        [CustomAuthorize]
        [HttpPost]
        public bool CreatePost(JObject postDto)
        {
            try
            {
                var creatingPost = new PostDto
                {
                    PostTitle = postDto["PostTitle"].ToString(),
                    PostContent = postDto["PostContent"].ToString(),
                    CreationDate = DateTime.Now,
                    RelatedTo = postDto["RelatedTo"].ToObject<BlogDto>(),
                    PostCategories = postDto["PostCategories"].ToObject<List<CategoryDto>>()
                };

                var result = PostService.CreatePost(creatingPost);

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


        [Route("updatepost")]
        [CustomAuthorize]
        [HttpPost]
        public bool UpdatePost(JObject postDto)
        {
            try
            {
                var updatedPost = new PostDto
                {
                    Id = Convert.ToInt32(postDto["Id"].ToString()),
                    PostTitle = postDto["PostTitle"].ToString(),
                    PostContent = postDto["PostContent"].ToString(),
                    CreationDate = DateTime.Now,
                    PostCategories = postDto["PostCategories"].ToObject<List<CategoryDto>>()
                };
                var result = PostService.UpdatePost(updatedPost);

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


        [Route("deletepost")]
        [CustomAuthorize]
        [HttpPost]
        public bool DeletePost(JObject postDto)
        {
            try
            {
                var result = PostService.DeletePost(new PostDto
                {
                    Id = Convert.ToInt32(postDto["Id"].ToString()),
                });

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


        [Route("getblogposts/{blogId}")]
        [HttpGet]
        public IEnumerable<PostDto> GetUserPosts(int blogId)
        {
            try
            {
                var result = PostService.GetBlogPosts(blogId);
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


        [Route("getpost/{postId}")]
        [HttpGet]
        public PostDto GetPost(int postId)
        {
            try
            {
                var result = PostService.GetPostById(postId);
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
    }
}
