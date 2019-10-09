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
    [WebService(Namespace = "http://bloguser.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AsmxBlogUserService : WebService, IAsmxBlogUserService
    {
        [Inject]
        public IBlogUserService BlogUserService { get; set; }

        public AsmxBlogUserService()
            : this(((NinjectHttpApplication)HttpContext.Current.ApplicationInstance).Kernel.Get<IBlogUserService>())
        {
        }

        public AsmxBlogUserService(IBlogUserService blogUserService)
        {
            BlogUserService = blogUserService;
        }


        [WebMethod]
        public int CreateBlogUser(BlogUserSoap bloguserSoap)
        {
            try
            {
                return BlogUserService.CreateBlogUser(new BlogUserDto
                {
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword,
                    BlogName = bloguserSoap.BlogName
                });
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public int UpdateBlogUser(BlogUserSoap bloguserSoap)
        {
            try
            {
                return BlogUserService.UpdateBlogUser(new BlogUserDto
                {
                    Id = bloguserSoap.Id,
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword,
                    BlogName = bloguserSoap.BlogName
                });
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public int DeleteBlogUser(BlogUserSoap bloguserSoap)
        {
            try
            {
                return BlogUserService.DeleteBlogUser(new BlogUserDto
                {
                    Id = bloguserSoap.Id
                });
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public int ChangePassword(BlogUserSoap bloguserSoap)
        {
            try
            {
                return BlogUserService.ChangePassword(new BlogUserDto
                {
                    Id = bloguserSoap.Id,
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword,
                });
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public BlogUserSoap[] GetAllUsers()
        {
            try
            {
                List<BlogUserSoap> bloguserSoapList = new List<BlogUserSoap>();
                IEnumerable<BlogUserDto> bloguserDtoList = BlogUserService.GetAllUsers();
                foreach (BlogUserDto bloguser in bloguserDtoList)
                {
                    bloguserSoapList.Add(new BlogUserSoap
                    {
                        Id = bloguser.Id,
                        UserName = bloguser.UserName,
                        UserPassword = bloguser.UserPassword,
                        BlogName = bloguser.BlogName
                    });
                }
                return bloguserSoapList.ToArray();
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public BlogUserSoap GetBlogUserById(int userId)
        {
            try
            {
                BlogUserDto bloguserDto = BlogUserService.GetBlogUserById(userId);
                return new BlogUserSoap
                {
                    Id = bloguserDto.Id,
                    UserName = bloguserDto.UserName,
                    UserPassword = bloguserDto.UserPassword,
                    BlogName = bloguserDto.BlogName
                };
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public BlogUserSoap GetBlogUserByName(string userName)
        {
            try
            {
                BlogUserDto bloguserDto = BlogUserService.GetBlogUserByName(userName);
                return new BlogUserSoap
                {
                    Id = bloguserDto.Id,
                    UserName = bloguserDto.UserName,
                    UserPassword = bloguserDto.UserPassword,
                    BlogName = bloguserDto.BlogName
                };
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public BlogUserSoap GetBlogUserNameAndPassword(BlogUserSoap incomingUser)
        {
            try
            {
                BlogUserDto bloguserDto = BlogUserService.GetBlogUserNameAndPassword(new BlogUserDto
                {
                    Id = incomingUser.Id,
                    UserName = incomingUser.UserName,
                    UserPassword = incomingUser.UserPassword,
                    BlogName = incomingUser.BlogName
                });
                return new BlogUserSoap
                {
                    Id = bloguserDto.Id,
                    UserName = bloguserDto.UserName,
                    UserPassword = bloguserDto.UserPassword,
                    BlogName = bloguserDto.BlogName
                };
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }


        [WebMethod]
        public int GetAdminPermission(string userName)
        {
            try
            {
                return BlogUserService.GetAdminPermission(userName);
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }
    }
}
