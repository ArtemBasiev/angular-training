using System;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Abstract
{
    public interface IBlogBuilder
    {
        void SetBlogUser(Blog blog, BlogUser blogUserToSet);

        void SetBlogId(Blog blog, int blogIdToSet);

        Blog CreateBlog(string blogName, DateTime creationDate);
    }
}