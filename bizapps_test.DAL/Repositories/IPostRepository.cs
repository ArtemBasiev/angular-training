using System.Collections.Generic;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        IEnumerable<Post> GetPostsByBlogId(int blogId);

        IEnumerable<Post> GetPostsByCategoryId(int categoryId);

        void AddCategoryToPost(Post post, Category categoryToAdd);

        void RemoveCategoryFromPost(Post post, Category categoryToRemove);
    }
}