using System;
using System.Drawing;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.DTO;

namespace bizapps_test.WEB
{
    public partial class CategoryUpdateDelete : System.Web.UI.Page
    {

        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null && Request.Cookies["perm"] != null)
            {
                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                {
                    if (!IsPostBack)
                    {
                        CategoryDto updatingCategory = CategoryService.GetCategoryById(Convert.ToInt32(Request.QueryString["CategoryId"]));
                        textboxCategoryName.Text = updatingCategory.CategoryName;
                    }
                }
                //else
                //{
                //    //return;
                //}
            }
            else
            {
                Response.Redirect("~/MainPage.aspx");
            }

        
        }

        protected void ButtonUpdateCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryService.UpdateCategory(new CategoryDto
                {
                    Id = Convert.ToInt32(Request.QueryString["CategoryId"]),
                    CategoryName = textboxCategoryName.Text
                });
                Response.Redirect("~/CategoryCreation.aspx");
            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = ex.Message;
            }
        }

        protected void ButtonDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryService.DeleteCategory(new CategoryDto { Id = Convert.ToInt32(Request.QueryString["CategoryId"]) });
                Response.Redirect("~/CategoryCreation.aspx");
            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = ex.Message;
            }
        }
    }
}