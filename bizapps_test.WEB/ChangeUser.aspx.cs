using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bizapps_test.BLL.Services;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.WEB.SpecialItems;
using System.Collections;
using System.Drawing;
using Ninject;

namespace bizapps_test.WEB
{
    public partial class ChangeUser : System.Web.UI.Page
    {
        [Ninject.Inject]
        public IBlogUserService bloguserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //bloguserService = new BlogUserService();
                BlogUserDto bloguserDTO = bloguserService.GetBlogUserById(Convert.ToInt32(Session["UserId"]));
                TextBoxUserName.Text = bloguserDTO.UserName;
                TextBoxUserPassword.Text = bloguserDTO.UserPassword;
                TextBoxBlogName.Text = bloguserDTO.BlogName;
            }
          
        }

        protected void ButtonDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                //bloguserService = new BlogUserService(); 
                bloguserService.DeleteBlogUser(new BlogUserDto { Id = Convert.ToInt32(Session["UserId"]) });
                Response.Redirect("~/Default.aspx");
            }
            catch(Exception ex)
            {
                LabelMes.BackColor = Color.Red;
                LabelMes.Text = ex.Message;

            }
            
        }

        protected void ButtonChangeUser_Click(object sender, EventArgs e)
        {
            try
            {
                //bloguserService = new BlogUserService(); 
                bloguserService.UpdateBlogUser(new BlogUserDto 
                   {
                    Id = Convert.ToInt32(Session["UserId"]) ,
                    UserName = TextBoxUserName.Text,
                     UserPassword = TextBoxUserPassword.Text,
                     BlogName = TextBoxBlogName.Text
                   });
                Response.Redirect("~/MainPage.aspx");
            }
            catch (Exception ex)
            {
                LabelMes.BackColor = Color.Red;
                LabelMes.Text = ex.Message;

            }
        }
    }
}