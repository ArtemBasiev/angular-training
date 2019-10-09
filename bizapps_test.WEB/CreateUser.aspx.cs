using System;
using System.Web;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;

namespace bizapps_test.WEB
{
    public partial class CreateUser : System.Web.UI.Page
    {
        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSignIn_OnClick(object sender, EventArgs e)
        {
            try
            {
                BlogUserDto bloguserDto = new BlogUserDto
                {
                    UserName = textboxUserName.Text,
                    UserPassword = textboxPassword.Text,
                    BlogName = textboxUserName.Text
                };
                BlogUserService.CreateBlogUser(bloguserDto);
                HttpCookie login = new HttpCookie("login", bloguserDto.UserName);
                HttpCookie sign = new HttpCookie("sign", SignGenerator.GetSign(bloguserDto.UserName + "byte"));

                try
                {
                    BlogUserService.GetAdminPermission(bloguserDto.UserName);
                    HttpCookie perm = new HttpCookie("perm", "admin");
                    Response.Cookies.Add(perm);
                }
                catch (Exception)
                {

                }

                Response.Cookies.Add(login);
                Response.Cookies.Add(sign);
                Response.Redirect("~/MainPage.aspx");

            }
            catch (Exception ex)
            {
                LabelMes.Text = ex.Message;
            }
        }
    }
}