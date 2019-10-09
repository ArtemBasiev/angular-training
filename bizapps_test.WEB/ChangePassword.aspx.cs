using System;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;

namespace bizapps_test.WEB
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] == null || Request.Cookies["sign"] == null)
            {

               Response.Redirect("~/MainPage.aspx");
            }
        }

        protected void ButtonChangePassword_OnClick(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null)
            {

                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                {
                    try
                    {
                        BlogUserService.ChangePassword(new BlogUserDto
                        {
                            UserName = Request.Cookies["login"].Value,
                            UserPassword = textboxNewPassword.Text
                        });
                        Response.Redirect("~/MainPage.aspx");
                    }
                    catch (Exception ex)
                    {
                        LabelMes.Text = ex.Message;
                    }

                }
            }
        }
    }
}