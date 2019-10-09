using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Concrete
{
    public class BlogUserBuilder : IBlogUserBuilder
    {
        public BlogUser CreateBlogUser(string userName, string userPassword)
        {
            return new BlogUser(userName, userPassword);
        }


        public void SetBlogUserId(BlogUser blogUser, int blogUserIdToSet)
        {
            if (blogUser.Id <= 0)
                blogUser.Id = blogUserIdToSet;
        }


        public void SetUserBlog(BlogUser blogUser, Blog blogToSet)
        {
            blogUser.UserBlog = blogToSet;
        }
    }
}