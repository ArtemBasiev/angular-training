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
    public partial class Default : System.Web.UI.Page
    {
        [Ninject.Inject]
        public IBlogUserService bloguserService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //----------------Заполняем список пользователей----------------------
            if(!IsPostBack)
            {
                try
                {
                    //UserList.Items.Clear();



                    foreach (var i in bloguserService.GetAllUsers())
                    {
                        ListItem userItem = new ListItem();
                        userItem.Text = i.UserName.ToString();
                        userItem.Value = i.Id.ToString();
                        UserList.Items.Add(userItem);

                    }


                }
                catch (Exception ex)
                {
                    LabelUsers.ForeColor = Color.Red;
                    LabelUsers.Text = ex.Message;
                }
            }
            
        }

        protected void UserList_Click(object sender, BulletedListEventArgs e)
        {
            Session["UserId"] = Convert.ToInt32(UserList.Items[e.Index].Value);
            Response.Redirect("~/MainPage.aspx");
            //Response.Redirect("~/MainPage.aspx?userName="+userName);
            
        }
    }
}