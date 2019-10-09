using System.Collections.Generic;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetBlogCategories(int blogId);

        IEnumerable<Category> GetPostCategories(int postId);
    }
}