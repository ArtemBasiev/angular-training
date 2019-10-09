using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;

namespace bizapps_test.WEB
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }

        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["user"]=="admin")
            //{
                if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null)
                {
                    if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                    {
                        AuthForm.Visible = false;
                    }
                }
                else
                {
                    AuthForm.Visible = true;
                }
               
            //}

            if (!IsPostBack)
            {
               List<CategoryDto> categories = (List<CategoryDto>)CategoryService.GetBlogCategories("admin");

                foreach (CategoryDto category in categories)
                {
                    ListItem listitem = new ListItem(category.CategoryName, category.Id.ToString());
                    CategoryList.Items.Add(listitem);
                }

                foreach (ListItem categoryItem in CategoryList.Items)
                {
                    if (categoryItem.Value == Request.QueryString["category"])
                    {
                        categoryItem.Attributes["Class"] = "activeitem";
                    }
                }
            }


            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null)
            {
                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value+"byte"))
                {
                    LabelUserName.Text = Request.Cookies["login"].Value;
                    LogoutPanel.Visible = true;
                    if (Request.Cookies["perm"] != null)
                    {
                        adminpanel.Visible = true;
                    }
                    

                }
            }

        

        }

        protected void CategoryList_OnClick(object sender, BulletedListEventArgs e)
        {
           Response.Redirect("~/MainPage.aspx?category="+CategoryList.Items[e.Index].Value);
            
          
        }

        protected void AuthForm_OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
 
                try
                {
                    BlogUserDto bloguserDto = new BlogUserDto();
                    bloguserDto.UserName = AuthForm.UserName;
                    bloguserDto.UserPassword = AuthForm.Password;

                    BlogUserDto newbloguserDto = BlogUserService.GetBlogUserNameAndPassword(bloguserDto);
                    HttpCookie login = new HttpCookie("login", newbloguserDto.UserName);
                    HttpCookie sign = new HttpCookie("sign", SignGenerator.GetSign(newbloguserDto.UserName + "byte"));

                    try
                    {
                    BlogUserService.GetAdminPermission(newbloguserDto.UserName);
                    HttpCookie perm = new HttpCookie("perm", "admin");
                    Response.Cookies.Add(perm);
                }
                    catch (Exception)
                    {
                        
                    }

                    Response.Cookies.Add(login);
                    Response.Cookies.Add(sign);
                    e.Authenticated = true;
                }
                catch (Exception)
                {
                    e.Authenticated = false;
                }

        }

        protected void AuthForm_OnLoggedIn(object sender, EventArgs e)
        {
            if (Request.Url.ToString().Contains("ViewPostPage.aspx"))
            {
                Response.Redirect("~/ViewPostPage.aspx?PostId="+Request.QueryString["PostId"]);
            }

            Response.Redirect("~/MainPage.aspx");
        }


        protected void ButtonLogOut_OnServerClick(object sender, EventArgs e)
        {
            if (Response.Cookies["login"] != null&& Response.Cookies["sign"] != null)
            {
                Response.Cookies["login"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["sign"].Expires = DateTime.Now.AddDays(-1);
                if (Response.Cookies["perm"] != null)
                {
                    Response.Cookies["perm"].Expires = DateTime.Now.AddDays(-1);
                }
                if (Request.Url.ToString().Contains("ViewPostPage.aspx"))
                {
                    Response.Redirect("~/ViewPostPage.aspx?PostId=" + Request.QueryString["PostId"]);
                }

                Response.Redirect("~/MainPage.aspx");
            }
        }
    }
}