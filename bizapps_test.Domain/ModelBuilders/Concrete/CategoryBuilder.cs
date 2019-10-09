using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Concrete
{
    public class CategoryBuilder : ICategoryBuilder
    {
        public void SetBlogRelatedTo(Category category, Blog blogRelatedTo)
        {
            category.RelatedTo = blogRelatedTo;
        }


        public void SetCategoryId(Category category, int categoryIdToSet)
        {
            category.Id = categoryIdToSet;
        }


        public Category CreateCategory(string categoryName)
        {
            return new Category(categoryName);
        }
    }
}