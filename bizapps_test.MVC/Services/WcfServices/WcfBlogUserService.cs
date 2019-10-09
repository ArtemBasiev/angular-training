using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.WcfBlogUserServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.WcfServices
{
    public class WcfBlogUserService: IBlogUserService
    {
       private WcfBlogUserServiceClient BlogUserServiceClient { get; }

        public WcfBlogUserService()
        {
            BlogUserServiceClient = new WcfBlogUserServiceClient();
        }

        public int CreateBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                return BlogUserServiceClient.CreateBlogUser(new BlogUserDC
                {
                    UserName = bloguserViewModel.UserName,
                    UserPassword = bloguserViewModel.UserPassword
                });
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public int UpdateBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                return BlogUserServiceClient.UpdateBlogUser(new BlogUserDC
                {
                    Id = bloguserViewModel.Id,
                    UserName = bloguserViewModel.UserName,
                    UserPassword = bloguserViewModel.UserPassword
                });
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public int DeleteBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                return BlogUserServiceClient.DeleteBlogUser(new BlogUserDC
                {
                    Id = bloguserViewModel.Id
                });
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public int ChangePassword(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                return BlogUserServiceClient.ChangePassword(new BlogUserDC
                {
                    Id = bloguserViewModel.Id,
                    UserName = bloguserViewModel.UserName,
                    UserPassword = bloguserViewModel.UserPassword,
                });
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public IEnumerable<BlogUserViewModel> GetAllUsers()
        {
            try
            {
                List<BlogUserViewModel> bloguserViewModelList = new List<BlogUserViewModel>();
                IEnumerable<BlogUserDC> bloguserDCList = BlogUserServiceClient.GetAllUsers();
                foreach (BlogUserDC bloguser in bloguserDCList)
                {
                    bloguserViewModelList.Add(new BlogUserViewModel
                    {
                        Id = bloguser.Id,
                        UserName = bloguser.UserName,
                        UserPassword = bloguser.UserPassword
                    });
                }
                return bloguserViewModelList;
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public BlogUserViewModel GetBlogUserById(int userId)
        {
            try
            {
                BlogUserDC bloguserDC = BlogUserServiceClient.GetBlogUserById(userId);
                return new BlogUserViewModel
                {
                    Id = bloguserDC.Id,
                    UserName = bloguserDC.UserName,
                    UserPassword = bloguserDC.UserPassword
                };
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public BlogUserViewModel GetBlogUserByName(string userName)
        {
            try
            {
                BlogUserDC bloguserDC = BlogUserServiceClient.GetBlogUserByName(userName);
                return new BlogUserViewModel
                {
                    Id = bloguserDC.Id,
                    UserName = bloguserDC.UserName,
                    UserPassword = bloguserDC.UserPassword
                };
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public BlogUserViewModel GetBlogUserNameAndPassword(BlogUserViewModel incomingUser)
        {
            try
            {
                BlogUserDC bloguserDC = BlogUserServiceClient.GetBlogUserNameAndPassword(new BlogUserDC
                {
                    Id = incomingUser.Id,
                    UserName = incomingUser.UserName,
                    UserPassword = incomingUser.UserPassword
                });
                return new BlogUserViewModel
                {
                    Id = bloguserDC.Id,
                    UserName = bloguserDC.UserName,
                    UserPassword = bloguserDC.UserPassword
                };
            }
            catch (FaultException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public int GetAdminPermission(string userName)
        {
            try
            {
                return BlogUserServiceClient.GetAdminPermission(userName);
            }
            catch(FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}