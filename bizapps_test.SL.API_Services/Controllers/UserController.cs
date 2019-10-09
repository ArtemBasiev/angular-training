using System.Web.Http;
using bizapps_test.BLL.DTO;
using Ninject;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Attributes;

namespace bizapps_test.SL.API_Services.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        [Inject]
        public IUserService UserService { get; set; }


        [Route("createuser")]
        [HttpPost]
        public bool CreateUser([FromBody]BlogUserDto bloguserDto)
        {
            try
            {
                var result = UserService.CreateUser(bloguserDto);
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


        [Route("updateuser")]
        [CustomAuthorize]
        [HttpPost]
        public bool UpdateUser(BlogUserDto bloguserDto)
        {
            try
            {
                var result = UserService.UpdateUser(bloguserDto);
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


        [Route("deleteuser")]
        [CustomAuthorize]
        [HttpPost]
        public bool DeleteUser(BlogUserDto bloguserDto)
        {
            try
            {
                var result = UserService.DeleteUser(bloguserDto);
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



        [Route("getuserbyid/{userId}")]
        [HttpGet]
        public BlogUserDto GetUserById(int userId)
        {
            try
            {
                var result = UserService.GetUserById(userId);
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


        [Route("getuserbyname/{userName}")]
        [HttpGet]
        public BlogUserDto GetUserByName(string userName)
        {
            try
            {
                var result = UserService.GetUserByName(userName);
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
