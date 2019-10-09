using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.DTO;
using bizapps_test.WEB.SpecialItems;

namespace bizapps_test.WEB
{
    public partial class PostCreation : System.Web.UI.Page
    {
        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }
        [Ninject.Inject]
        public IPostService PostService { get; set; }

        //public string PostImage { get; set; } = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null && Request.Cookies["perm"] != null)
            {
                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                {
                    if (!IsPostBack)
                    {
                        preBodyHolder.InnerText = "<p><p/>   <p><p/>   <p><p/>  <p><p/>  <p><p/>";
                    }
                    try

                    {

                        foreach (var i in CategoryService.GetAllCategories())
                        {
                            CategoryCheckBox newCategoryCheckBox = new CategoryCheckBox(i.CategoryName, i.Id);
                            CategoryCheckBoxPanel.Controls.Add(newCategoryCheckBox);

                        }


                    }
                    catch (Exception ex)
                    {
                        LabelMes.ForeColor = Color.Red;
                        LabelMes.Text = ex.Message;
                    }
                }
                //else
                //{
                //    return;
                //}
            }
            else
            {
                Response.Redirect("~/MainPage.aspx");
            }
        
          
           

        }

        protected void ButtonCreatePost_Click(object sender, EventArgs e)
        {
            //----------------Создаем новый пост----------------------
            try
            {
                List<CategoryDto> postCategories = new List<CategoryDto>();

                foreach (CategoryCheckBox categoryItem in CategoryCheckBoxPanel.Controls)
                {
                    if (categoryItem.Checked)
                    {
                        postCategories.Add(new CategoryDto { Id = categoryItem.CategoryId});
                    }
                }

                PostService.CreatePost(new PostDto
                {
                    Title = textboxPostTitle.Text,
                    Body = HttpUtility.HtmlEncode(preBodyHolder.InnerText),
                    PostImage = ImageFileUpload.FileName
                }, 1,
                postCategories);

                Response.Redirect("~/MainPage.aspx");


            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = "Ошибка" + ex.Message;
            }
        }

        //protected void ImageFileUpload_OnDataBinding(object sender, EventArgs e)
        //{
        //    PostImage = ImageFileUpload.FileName;
        //}
    }
}