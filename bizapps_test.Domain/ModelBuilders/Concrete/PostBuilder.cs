using System;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Concrete
{
    public class PostBuilder : IPostBuilder
    {
        public Post CreatePost(string postTitle, string postContent, DateTime creationDate)
        {
            return new Post(postTitle, postContent, creationDate);
        }


        public void SetPostId(Post post, int postIdToSet)
        {
            post.Id = postIdToSet;
        }


        public void SetBlogRelatedTo(Post post, Blog blogRelatedTo)
        {
            post.RelatedTo = blogRelatedTo;
        }
    }
}