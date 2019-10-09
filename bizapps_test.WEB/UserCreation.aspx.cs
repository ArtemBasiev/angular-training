using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using Ninject;
using Ninject.Modules;
using bizapps_test.BLL.Infrastructure;
using bizapps_test.BLL.Interfaces;
using System.Web.Routing;
using bizapps_test.BLL.DTO;

namespace bizapps_test.WEB
{
    public partial class UserCreation : System.Web.UI.Page
    {
         [Ninject.Inject]
        public IBlogUserService bloguserService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateUser_Click(object sender, EventArgs e)
        {
            //----------------Создаем нового пользователя----------------------
            try
            {

                bloguserService.CreateBlogUser(new BlogUserDto
                {
                    UserName =LoginText.Text,
                    UserPassword = TextPassword.Text,
                    BlogName = TextBlog.Text
                });
                Response.Redirect("~/Default.aspx");


            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = "Ошибка" + ex.Message;
            }
        }
    }
}