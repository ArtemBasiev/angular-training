using System;
using System.Web.UI.WebControls;
using System.Drawing;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.DTO;



namespace bizapps_test.WEB
{
    public partial class CategoryCreation : System.Web.UI.Page
    {
        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["sign"] != null && Request.Cookies["perm"] != null)
            {
                if (Request.Cookies["sign"].Value == SignGenerator.GetSign(Request.Cookies["login"].Value + "byte"))
                {
                    try
                    {
                        CategoryList.Items.Clear();


                        foreach (var i in CategoryService.GetAllCategories())
                        {
                            CategoryList.Items.Add(new ListItem(i.CategoryName, i.Id.ToString()));
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
                //    //return;
                //}
                
            }
            else
            {
                Response.Redirect("~/MainPage.aspx");
            }


            //----------------Заполняем список категорий с помощью метода GetCategories----------------------
           
        }

        protected void cr_kat_Click(object sender, EventArgs e)
        {

            //----------------Создаем новую категорию с помощью метода MakeCategory класса CategoryOperations----------------------
            try
            {

                CategoryService.CreateCategory(new CategoryDto 
                {
                    CategoryName = textboxCategoryName.Text
                });         
                Response.Redirect(Request.Path);

                //LabelMes.Text = "Категория успешно добавлена";
                //LabelMes.ForeColor = Color.Green;
                

            }
            catch (Exception ex)
            {
                LabelMes.ForeColor = Color.Red;
                LabelMes.Text = "Ошибка" + ex.Message;
            }

        }


        protected void CategoryList_OnClick(object sender, BulletedListEventArgs e)
        {
            Response.Redirect("~/CategoryUpdateDelete.aspx?CategoryId=" + CategoryList.Items[e.Index].Value);
        }
    }
}