using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;

namespace bizapps_test.WEB
{
    public partial class MainPage : Page
    {
        [Ninject.Inject]
        public IBlogUserService BloguserService { get; set; }
        [Ninject.Inject]
        public IPostService PostService { get; set; }
         [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }
     

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindDataList();
            }



        }

        protected void BindDataList()
        {
                IEnumerable<PostDto> posts;

                string categoryOption;
              
                    
                    if (Request.QueryString["category"] == null)
                    {
                         categoryOption = "allcategories";
                    }
                    else
                    {
                        categoryOption = Request.QueryString["category"];
            }

                 


            if (categoryOption == "allcategories")
                {
                   posts = PostService.GetUserPostsByUserName("admin");
                }
                else
                {
                    if (categoryOption == "withoutcategory")
                    {
                        posts = PostService.GetPostsByUserNameWithoutCategory("admin");
                    }
                    else
                    {
                        posts = PostService.GetPostsByUserNameAndCategory("admin", Convert.ToInt32(Request.QueryString["category"]));
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("PostId");
                dt.Columns.Add("Title");
                dt.Columns.Add("CreationDate");
                dt.Columns.Add("PostImage");
                dt.Columns.Add("Categories");

                foreach (PostDto post in posts)
                {

                    string categoryString = "";
                    IEnumerable<CategoryDto> postCategories = CategoryService.GetPostCategories(post.Id);
                    foreach (CategoryDto category in postCategories)
                    {
                        categoryString += category.CategoryName + ", ";
                    }
                    if (categoryString.Trim() != "")
                    {
                        categoryString = categoryString.Remove(categoryString.Length - 2);
                    }

                    DataRow newRow = dt.NewRow();
                    newRow["Title"] = post.Title;
                    newRow["PostId"] = post.Id;
                    newRow["CreationDate"] = post.CreationDate.ToString("D");

              
              
                if (File.Exists(MapPath("Images/" + post.PostImage.Trim())))
                    {
                        newRow["PostImage"] = @"/Images/" + post.PostImage.Trim();
                    }
                    else
                    {
                    newRow["PostImage"] = "/Images/img-post-default.jpg";
                    }
                    

                    if (categoryString == "")
                    {
                        categoryString = "Without category";
                    }
                    newRow["Categories"] = categoryString;
                    dt.Rows.Add(newRow);


                }

                main_gridview.DataSource = dt;
                main_gridview.DataBind();

            //}
           

           

        }

        protected void main_gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            main_gridview.PageIndex = e.NewPageIndex;
            main_gridview.DataBind();
            BindDataList();
        }

        protected void ButtonShowMore_OnServerClick(object sender, EventArgs e)
        {
           var btn = (Control)sender;
           HiddenField hfPostId =  (HiddenField)btn.Parent.FindControl("PostId");
           Response.Redirect("~/ViewPostPage.aspx?PostId="+ hfPostId.Value);
        }

        public IEnumerable<int> GetPages()
        {
            return Enumerable.Range(1, main_gridview.PageCount);
        }

        protected void OnRepeaterCommand(object source, RepeaterCommandEventArgs e)
        {
            source.GetType()
                .GetMethod("RaiseBubbleEvent", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(source, new[]
                {
                    e.CommandSource,
                    new CommandEventArgs(e.CommandName, e.CommandArgument)
                });
        }



    }
}