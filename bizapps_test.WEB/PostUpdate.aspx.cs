using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.DTO;
using bizapps_test.WEB.SpecialItems;

namespace bizapps_test.WEB
{
    public partial class PostUpdate : System.Web.UI.Page
    {
        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }
        [Ninject.Inject]
        public IPostService PostService { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null && Request.Cookies["perm"] != null)
            {
                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                {
                    if (!IsPostBack)
                    {
                        try
                        {
                            PostDto updatedPost = PostService.GetPost(Convert.ToInt32(Request.QueryString["PostId"]));
                            textboxPostTitle.Text = updatedPost.Title;
                            preBodyHolder.InnerText = HttpUtility.HtmlDecode(updatedPost.Body);
                        }
                        catch (Exception ex)
                        {
                            LabelMes.ForeColor = Color.Red;
                            LabelMes.Text = ex.Message;
                        }


                    }


                    try
                    {
                        //------------------Заполняем список категорий поста---------------------------
                        foreach (var i in CategoryService.GetAllCategories())
                        {
                            int isEquals = 0;
                            foreach (var j in (List<CategoryDto>)CategoryService.GetPostCategories(Convert.ToInt32(Request.QueryString["PostId"])))
                            {
                                if (i.Id == j.Id)
                                {
                                    isEquals = 1;
                                }
                            }

                            if (isEquals == 1)
                            {
                                CategoryCheckBox newCategoryCheckBox = new CategoryCheckBox(i.CategoryName, i.Id, true);
                                CategoryCheckBoxPanel.Controls.Add(newCategoryCheckBox);

                            }


                            else
                            {
                                CategoryCheckBox newCategoryCheckBox = new CategoryCheckBox(i.CategoryName, i.Id);
                                CategoryCheckBoxPanel.Controls.Add(newCategoryCheckBox);
                            }
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

        protected void ButtonUpdatePost_Click(object sender, EventArgs e)
        {
           // ----------------Сохраняем изменения----------------------
            try
            {
                List<CategoryDto> postCategories = new List<CategoryDto>();

                foreach (CategoryCheckBox categoryItem in CategoryCheckBoxPanel.Controls)
                {
                    if (categoryItem.Checked)
                    {
                        postCategories.Add(new CategoryDto { Id = categoryItem.CategoryId });
                    }
                }


                PostDto newPost = new PostDto
                {
                    Id = Convert.ToInt32(Request.QueryString["PostId"]),
                    Title = textboxPostTitle.Text,
                    Body = HttpUtility.HtmlEncode(preBodyHolder.InnerText),
                    PostImage = string.Empty
                };

                if (ImageFileUpload.HasFile)
                {
                    newPost.PostImage = ImageFileUpload.FileName;
                }

                PostService.UpdatePost(newPost, postCategories);

                Response.Redirect("~/ViewPostPage.aspx?PostId="+Request.QueryString["PostId"]);


            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = "Ошибка" + ex.Message;
            }
        }

        protected void ButtonDeletePost_OnClick(object sender, EventArgs e)
        {
            try
            {
                PostService.DeletePost(new PostDto
                {
                    Id = Convert.ToInt32(Request.QueryString["PostId"])
                });
                Response.Redirect("~/MainPage.aspx");
            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = "Ошибка" + ex.Message;
            }
        }
    }
}