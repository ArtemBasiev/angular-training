using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Abstract
{
    public interface ICategoryBuilder
    {
        void SetBlogRelatedTo(Category category, Blog blogRelatedTo);

        void SetCategoryId(Category category, int categoryIdToSet);

        Category CreateCategory(string categoryName);
    }
}