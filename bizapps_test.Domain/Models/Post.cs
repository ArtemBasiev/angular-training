using System;
using System.Collections.Generic;

namespace bizapps_test.Domain.Models
{
    public class Post 
    {
        private readonly List<Category> _postCategories;

        private readonly List<Comment> _postComments;


        /// <exception cref="ArgumentNullException">
        ///     Thrown when one of the parameters is empty
        /// </exception>
        /// <param name="relatedTo">A blog this post is related to.</param>
        public Post(string postTitle, string postContent, DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(postTitle)) throw new ArgumentNullException("PostTitle cannot be empty!");
            PostTitle = postTitle;

            if (string.IsNullOrWhiteSpace(postContent)) throw new ArgumentNullException("PostContent cannot be empty!");
            PostContent = postContent;

            if ((creationDate == null) || (creationDate == DateTime.MinValue))
                throw new ArgumentNullException("CreationDate cannot be empty!");
            CreationDate = creationDate;

            _postCategories = new List<Category>();
            _postComments = new List<Comment>();
        }


        public int Id { get; internal set; }

        public string PostTitle { get; private set; }

        public string PostContent { get; private set; }

        public DateTime CreationDate { get; private set; }

        public Blog RelatedTo { get; internal set; }

        public IEnumerable<Category> PostCategories
        {
            get { return _postCategories; }
        }

        public IEnumerable<Comment> PostComments
        {
            get { return _postComments; }
        }


        public void AddCategory(Category categoryToAdd)
        {
            if (!ContainsCategoryInList(categoryToAdd))
                _postCategories.Add(categoryToAdd);
        }

        public void RemoveCategory(Category categoryToRemove)
        {
            if (ContainsCategoryInList(categoryToRemove))
                _postCategories.Remove(categoryToRemove);
        }


        public void AddComment(Comment commentToAdd)
        {
            if (!ContainsCommentInList(commentToAdd))
                _postComments.Add(commentToAdd);
        }

        public void RemoveComment(Comment commentToRemove)
        {
            if (ContainsCommentInList(commentToRemove))
                _postComments.Remove(commentToRemove);
        }

        private bool ContainsCategoryInList(Category containedCategory)
        {
            var isContains = false;
            foreach (var blogCategory in _postCategories)
            {
                if (blogCategory.CategoryName == containedCategory.CategoryName)
                {
                    isContains = true;
                }
            }

            return isContains;
        }


        private bool ContainsCommentInList(Comment containedComment)
        {
            var isContains = false;
            foreach (var postComment in _postComments)
            {
                if ((postComment.CommentText == containedComment.CommentText) &&
                    (postComment.CreationDate == containedComment.CreationDate))
                {
                    isContains = true;
                }
            }

            return isContains;
        }
    }
}