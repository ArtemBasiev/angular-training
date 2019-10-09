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
using Ninject;

namespace bizapps_test.WEB.SpecialItems
{
    public partial class ItemPost : System.Web.UI.UserControl
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostBody{ get; set; }
        public string PostCategories { get; set; }

      
        public IPostService postService { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.LabelPostTitle.Text = PostTitle;
            this.LabelCategory.Text = PostCategories;
            this.LiteralBody.Text = PostBody;
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            //----------------Удаляем пост----------------------
            //try
            //{


            postService.DeletePost(new PostDto
            {
                    Id = PostId
                });
                Response.Redirect(Request.Path);


            //}
            //catch (Exception ex)
            //{
                //LabelMes.ForeColor = Color.Red;
            //    LabelMes.Text = "Ошибка" + ex.Message;
            //}
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PostUpdate.aspx?PostId="+PostId.ToString());
        }

    }
}