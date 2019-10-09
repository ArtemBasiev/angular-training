using System;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Concrete
{
    public class BlogBuilder : IBlogBuilder
    {
        public void SetBlogUser(Blog blog, BlogUser blogUserToSet)
        {
            blog.CreatedBy = blogUserToSet;
        }

        public void SetBlogId(Blog blog, int blogIdToSet)
        {
            if (blog.Id <= 0)
                blog.Id = blogIdToSet;
        }

        public Blog CreateBlog(string blogName, DateTime creationDate)
        {
            return new Blog(blogName, creationDate);
        }
    }
}