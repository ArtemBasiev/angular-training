using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.AsmxBlogUserServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.AsmxServices
{
    public class AsmxBlogUserService: IBlogUserService
    {
        private AsmxBlogUserServiceSoapClient BlogUserServiceClient { get; }

        public AsmxBlogUserService()
        {
            BlogUserServiceClient = new AsmxBlogUserServiceSoapClient();
        }

        public int CreateBlogUser(BlogUserViewModel bloguserViewModel)
        {
            try
            {
                return BlogUserServiceClient.CreateBlogUser(new BlogUserSoap
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
                return BlogUserServiceClient.UpdateBlogUser(new BlogUserSoap
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
                return BlogUserServiceClient.DeleteBlogUser(new BlogUserSoap
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
                return BlogUserServiceClient.ChangePassword(new BlogUserSoap
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
                IEnumerable<BlogUserSoap> bloguserSoapList = BlogUserServiceClient.GetAllUsers();
                foreach (BlogUserSoap bloguser in bloguserSoapList)
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
                BlogUserSoap bloguserSoap = BlogUserServiceClient.GetBlogUserById(userId);
                return new BlogUserViewModel
                {
                    Id = bloguserSoap.Id,
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword
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
                BlogUserSoap bloguserSoap = BlogUserServiceClient.GetBlogUserByName(userName);
                return new BlogUserViewModel
                {
                    Id = bloguserSoap.Id,
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword
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
                BlogUserSoap bloguserSoap = BlogUserServiceClient.GetBlogUserNameAndPassword(new BlogUserSoap
                {
                    Id = incomingUser.Id,
                    UserName = incomingUser.UserName,
                    UserPassword = incomingUser.UserPassword
                });
                return new BlogUserViewModel
                {
                    Id = bloguserSoap.Id,
                    UserName = bloguserSoap.UserName,
                    UserPassword = bloguserSoap.UserPassword
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
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}