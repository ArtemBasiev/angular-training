using System;
using System.Collections.Generic;

namespace bizapps_test.Domain.Models
{
    public class Comment 
    {

        /// <exception cref="ArgumentNullException">
        ///     Thrown when one of the parameters is empty
        /// </exception>
        /// <param name="relatedTo">A post this comment is related to.</param>
        /// <param name="createdBy">A user this comment is created by.</param>
        public Comment(string commentText, DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(commentText)) throw new ArgumentNullException("CommentText cannot be empty!");
            CommentText = commentText;

            if ((creationDate == null) || (creationDate == DateTime.MinValue))
                throw new ArgumentNullException("CreationDate cannot be empty!");
            CreationDate = creationDate;

        }


        public int Id { get; internal set; }

        public string CommentText { get; }

        public DateTime CreationDate { get; }

        public Post RelatedTo { get; internal set; }

        public BlogUser CreatedBy { get; internal set; }

    }
}