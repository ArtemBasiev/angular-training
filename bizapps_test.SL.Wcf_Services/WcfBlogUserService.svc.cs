using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.Wcf_Services.DataContracts;
using bizapps_test.SL.Wcf_Services.ExceptionHandlers;
using Ninject;


namespace bizapps_test.SL.Wcf_Services
{
    
    public class WcfBlogUserService : IWcfBlogUserService
    {
        [Inject]
        public IBlogUserService BlogUserService { get; set; }


        public int CreateBlogUser(BlogUserDC blogUserDC)
        {
            try
            {
                 BlogUserService.CreateEntity(new BlogUserDto
                {
                    UserName = blogUserDC.UserName,
                    UserPassword = blogUserDC.UserPassword
                });

                return 1;
            }
            catch (ApplicationException e)
            {
                throw new FaultException(e.Message);
            }
        }

        //public int UpdateBlogUser(BlogUserDC bloguserDC)
        //{
        //    try
        //    {
        //        return BlogUserService.UpdateBlogUser(new BlogUserDto
        //        {
        //            Id = bloguserDC.Id,
        //            UserName = bloguserDC.UserName,
        //            UserPassword = bloguserDC.UserPassword,
        //            BlogName = bloguserDC.BlogName
        //        });
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}


        //public int DeleteBlogUser(BlogUserDC bloguserDC)
        //{
        //    try
        //    {
        //        return BlogUserService.DeleteBlogUser(new BlogUserDto
        //        {
        //            Id = bloguserDC.Id
        //        });
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public int ChangePassword(BlogUserDC bloguserDC)
        //{
        //    try
        //    {
        //        return BlogUserService.ChangePassword(new BlogUserDto
        //        {
        //            Id = bloguserDC.Id,
        //            UserName = bloguserDC.UserName,
        //            UserPassword = bloguserDC.UserPassword,
        //        });
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public IEnumerable<BlogUserDC> GetAllUsers()
        //{
        //    try
        //    {
        //        List<BlogUserDC> bloguserDCList = new List<BlogUserDC>();
        //        IEnumerable<BlogUserDto> bloguserDtoList = BlogUserService.GetAllUsers();
        //        foreach (BlogUserDto bloguser in bloguserDtoList)
        //        {
        //            bloguserDCList.Add(new BlogUserDC
        //            {
        //                Id = bloguser.Id,
        //                UserName = bloguser.UserName,
        //                UserPassword = bloguser.UserPassword,
        //                BlogName = bloguser.BlogName
        //            });
        //        }
        //        return bloguserDCList;
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}


        //public BlogUserDC GetBlogUserById(int userId)
        //{
        //    try
        //    {
        //        BlogUserDto bloguserDto = BlogUserService.GetBlogUserById(userId);
        //        return new BlogUserDC
        //        {
        //            Id = bloguserDto.Id,
        //            UserName = bloguserDto.UserName,
        //            UserPassword = bloguserDto.UserPassword,
        //            BlogName = bloguserDto.BlogName
        //        };
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public BlogUserDC GetBlogUserByName(string userName)
        //{
        //    try
        //    {
        //        BlogUserDto bloguserDto = BlogUserService.GetBlogUserByName(userName);
        //        return new BlogUserDC
        //        {
        //            Id = bloguserDto.Id,
        //            UserName = bloguserDto.UserName,
        //            UserPassword = bloguserDto.UserPassword,
        //            BlogName = bloguserDto.BlogName
        //        };
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public BlogUserDC GetBlogUserNameAndPassword(BlogUserDC incomingUser)
        //{
        //    try
        //    {
        //        BlogUserDto bloguserDto = BlogUserService.GetBlogUserNameAndPassword(new BlogUserDto
        //        {
        //            Id = incomingUser.Id,
        //            UserName = incomingUser.UserName,
        //            UserPassword = incomingUser.UserPassword,
        //            BlogName = incomingUser.BlogName
        //        });
        //        return new BlogUserDC
        //        {
        //            Id = bloguserDto.Id,
        //            UserName = bloguserDto.UserName,
        //            UserPassword = bloguserDto.UserPassword,
        //            BlogName = bloguserDto.BlogName
        //        };
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //[GlobalErrorBehavior(typeof(GlobalErrorHandler))]
        //public int GetAdminPermission(string userName)
        //{
        //    try
        //    {
        //        return BlogUserService.GetAdminPermission(userName);
        //    }
        //    catch (ApplicationException e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}
    }
}
