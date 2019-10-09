using System.Web.UI.WebControls;

namespace bizapps_test.WEB.SpecialItems
{
    public class CategoryCheckBox: CheckBox
    {
       public int CategoryId { get; set; }



        public CategoryCheckBox(string categoryName, int categoryId, bool isChecked)
        {
            Text = categoryName;
            CategoryId = categoryId;
            Checked = isChecked;
        }

        public CategoryCheckBox(string categoryName, int categoryId)
        {
            Text = categoryName;
            CategoryId = categoryId;
        }


        public CategoryCheckBox()
        {

        }


    }
}