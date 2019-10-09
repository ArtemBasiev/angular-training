using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.SL.ASMX_Services.SoapEntities;

namespace bizapps_test.SL.ASMX_Services.Interfaces
{
    public interface IAsmxCategoryService
    {
        int CreateCategory(CategorySoap categorySoap);

        int UpdateCategory(CategorySoap categorySoap);

        int DeleteCategory(CategorySoap categorySoap);

        CategorySoap[] GetAllCategories();

        CategorySoap[] GetPostCategories(int postId);

        CategorySoap[] GetBlogCategories(string userName);

        CategorySoap GetCategoryById(int categoryId);
    }
}
