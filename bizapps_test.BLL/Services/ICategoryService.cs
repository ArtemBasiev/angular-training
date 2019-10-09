using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.DAL.Repositories;

namespace bizapps_test.BLL.Services
{
    public interface ICategoryService
    {
        AnswerStatus CreateCategory(CategoryDto categoryDTO);

        AnswerStatus CreateCategory(CategoryDto categoryDTO, ICategoryRepository categoryRepository);

        AnswerStatus UpdateCategory(CategoryDto categoryDTO);

        AnswerStatus DeleteCategory(CategoryDto categoryDTO);

        AnswerStatus DeleteCategory(CategoryDto categoryDTO, ICategoryRepository categoryRepository);

        ServiceAnswer<CategoryDto> GetCategoryById(int categoryId);

        ServiceAnswer<IEnumerable<CategoryDto>> GetBlogCategories(int blogId);

        ServiceAnswer<IEnumerable<CategoryDto>> GetPostCategories(int postId);
    }
}