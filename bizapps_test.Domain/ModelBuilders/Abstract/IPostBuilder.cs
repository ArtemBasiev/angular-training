using System;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Abstract
{
    public interface IPostBuilder
    {
        Post CreatePost(string postTitle, string postContent, DateTime creationDate);

        void SetPostId(Post post, int postIdToSet);

        void SetBlogRelatedTo(Post post, Blog blogRelatedTo);
    }
}