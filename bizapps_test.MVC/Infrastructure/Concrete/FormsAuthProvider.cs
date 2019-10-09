using System;
using System.Web;
using System.Web.Security;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Infrastructure.Abstract;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        private FormsAuthenticationTicket AuthTicket { get; set; }

        public bool Authenticate(string userName, string password)
        {
            try
            {
                BlogUserViewModel newbloguserViewModel = BlogUserService.GetBlogUserNameAndPassword(new BlogUserViewModel
                {
                    UserName = userName,
                    UserPassword = password
                });
                try
                {
                    BlogUserService.GetAdminPermission(newbloguserViewModel.UserName);
                    AuthTicket = new FormsAuthenticationTicket(
                        1,
                        userName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        false,
                        "Admin"
                    );
                }
                catch (Exception)
                {
                    AuthTicket = new FormsAuthenticationTicket(
                        1,
                        userName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        false,
                        "User"
                    );
                }
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(AuthTicket));
                HttpContext.Current.Response.Cookies.Add(authCookie);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public string GetCurrentUser()
        {
            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request
                    .Cookies[FormsAuthentication.FormsCookieName].Value);
                return ticket.Name;
            }

            catch
            {
                return null;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}