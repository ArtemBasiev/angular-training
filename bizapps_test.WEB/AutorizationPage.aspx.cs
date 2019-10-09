using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bizapps_test.BLL.Services;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.WEB.SpecialItems;
using System.Collections;

namespace bizapps_test.WEB
{
    public partial class AutorizationPage : System.Web.UI.Page
    {
        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                BlogUserDto bloguserDto = new BlogUserDto();
                bloguserDto.UserName = this.TextboxLogin.Text;
                bloguserDto.UserPassword = this.PasswordBox.Value;

                BlogUserDto newbloguserDto = BlogUserService.GetBlogUserNameAndPassword(bloguserDto);

                HttpCookie login = new HttpCookie("login", newbloguserDto.UserName);
                HttpCookie sign = new HttpCookie("sign", SignGenerator.GetSign(newbloguserDto.UserName+"byte"));

                Response.Cookies.Add(login);
                Response.Cookies.Add(sign);

                Response.Redirect("~/MainPage.aspx");
            }
            catch(Exception ex)
            {
                this.PasswordHelp.Text = ex.Message;
            }
            


        }
    }
}