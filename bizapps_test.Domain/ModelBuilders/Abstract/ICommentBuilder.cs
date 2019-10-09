using System;
using bizapps_test.Domain.Models;

namespace bizapps_test.Domain.ModelBuilders.Abstract
{
    public interface ICommentBuilder
    {
        Comment CreateComment(string commentText, DateTime creationDate);

        void SetCommentId(Comment comment, int commentIdToSet);

        void SetPostRelatedTo(Comment comment, Post postRelatedTo);

        void SetUserCreatedBy(Comment comment, BlogUser userCreatedBy);
    }
}