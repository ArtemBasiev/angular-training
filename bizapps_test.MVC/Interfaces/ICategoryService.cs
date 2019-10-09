using System.Collections.Generic;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Interfaces
{
   public interface ICategoryService
    {
        int CreateCategory(CategoryViewModel categoryDto);

        int UpdateCategory(CategoryViewModel categoryDto);

        int DeleteCategory(CategoryViewModel categoryDto);

        IEnumerable<CategoryViewModel> GetAllCategories();

        IEnumerable<CategoryViewModel> GetPostCategories(int postId);

        IEnumerable<CategoryViewModel> GetBlogCategories(string userName);

        CategoryViewModel GetCategoryById(int categoryId);
    }
}
