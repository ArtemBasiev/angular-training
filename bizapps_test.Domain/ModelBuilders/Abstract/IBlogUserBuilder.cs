using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Abstract
{
    public interface IBlogUserBuilder
    {
        BlogUser CreateBlogUser(string userName, string userPassword);
        void SetBlogUserId(BlogUser blogUser, int blogUserIdToSet);
        void SetUserBlog(BlogUser blogUser, Blog blogToSet);
    }
}