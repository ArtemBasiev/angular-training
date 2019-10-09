using System.Collections.Generic;
using System.Web.Http;
using bizapps_test.BLL.DTO;
using Ninject;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Attributes;

namespace bizapps_test.SL.API_Services.Controllers
{
    [RoutePrefix("category")]
    public class CategoryController : ApiController
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Route("createcategory")]
        [CustomAuthorize]
        [HttpPost]
        public bool CreateCategory([FromBody]CategoryDto categoryDto)
        {
            try
            {
                var result = CategoryService.CreateCategory(categoryDto);

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


        [Route("updatecategory")]
        [CustomAuthorize]
        [HttpPost]
        public bool UpdateCategory([FromBody]CategoryDto categoryDto)
        {
            try
            {
                var result = CategoryService.UpdateCategory(categoryDto);

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


        [Route("deletecategory")]
        [CustomAuthorize]
        [HttpPost]
        public bool DeleteCategory([FromBody]CategoryDto categoryDto)
        {
            try
            {
                var result = CategoryService.DeleteCategory(categoryDto);

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


        [Route("getpostcategories/{postId:int}")]
        [HttpGet]
        public IEnumerable<CategoryDto> GetPostCategories(int postId)
        {
            try
            {
                var result = CategoryService.GetPostCategories(postId);
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


        [Route("getblogcategories/{blogId:int}")]
        [HttpGet]
        public IEnumerable<CategoryDto> GetBlogCategories(int blogId)
        {
            try
            {
                var result = CategoryService.GetBlogCategories(blogId);
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


        [Route("getcategorybyid/{categoryId:int}")]
        [HttpGet]
        public CategoryDto GetCategoryById(int categoryId)
        {
            try
            {
                var result = CategoryService.GetCategoryById(categoryId);
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
