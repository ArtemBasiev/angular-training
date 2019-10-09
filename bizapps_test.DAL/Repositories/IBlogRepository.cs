using System.Collections.Generic;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        Blog GetBlogByUserId(int userId);

        Blog GetBlogByCategoryId(int categoryId);

        Blog GetBlogByPostId(int postId);

        IEnumerable<Blog> GetAllBlogs();
    }
}