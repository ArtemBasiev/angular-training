using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories
{
    public interface IBlogUserRepository : IBaseRepository<BlogUser>
    {
        BlogUser GetBlogUserByName(string userName);

        BlogUser GetBlogUserByBlogId(int blogId);

        BlogUser GetUserByCommentId(int commentId);
    }
}