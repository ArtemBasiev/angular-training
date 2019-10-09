using System;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Concrete
{
    public class CommentBuilder : ICommentBuilder
    {
        public Comment CreateComment(string commentText, DateTime creationDate)
        {
            return new Comment(commentText, creationDate);
        }


        public void SetCommentId(Comment comment, int commentIdToSet)
        {
            comment.Id = commentIdToSet;
        }


        public void SetPostRelatedTo(Comment comment, Post postRelatedTo)
        {
            comment.RelatedTo = postRelatedTo;
        }

        public void SetUserCreatedBy(Comment comment, BlogUser userCreatedBy)
        {
            comment.CreatedBy = userCreatedBy;
        }
    }
}