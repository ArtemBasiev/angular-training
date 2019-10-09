using System;
using System.Collections.Generic;

namespace bizapps_test.Domain.Models
{
    public class Blog 
    {
        private readonly List<Category> _blogCategories;

        private List<Post> _blogPosts;


        /// <exception cref="ArgumentNullException">
        ///     Thrown when one of the parameters is empty
        /// </exception>
        /// <param name="blogCreator">A user created current blog.</param>
        internal Blog(string blogTitle, DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(blogTitle)) throw new ArgumentNullException("BlogName cannot be empty!");
            BlogTitle = blogTitle;

            if ((creationDate == null) || (creationDate == DateTime.MinValue))
                throw new ArgumentNullException("CreationDate cannot be empty!");
            CreationDate = creationDate;

            _blogCategories = new List<Category>();
            _blogPosts = new List<Post>();
        }


        public int Id { get; internal set; }

        public string BlogTitle { get; private set; }

        public DateTime CreationDate { get; private set; }

        public BlogUser CreatedBy { get; internal set; }

        public IEnumerable<Category> BlogCategories
        {
            get { return _blogCategories; }
        }

        public IEnumerable<Post> BlogPosts
        {
            get { return _blogPosts; }
        }

        public void AddCategory(Category categoryToAdd)
        {
            if (!ContainsCategoryInList(categoryToAdd))
                _blogCategories.Add(categoryToAdd);
        }

        public void RemoveCategory(Category categoryToRemove)
        {
            if (ContainsCategoryInList(categoryToRemove))
            {
                _blogCategories.Remove(categoryToRemove);
            }
        }


        public void AddPost(Post postToAdd)
        {
            if (!ContainsPostInList(postToAdd))
                _blogPosts.Add(postToAdd);
        }

        public void RemovePost(Post postToRemove)
        {
            if (ContainsPostInList(postToRemove))
            {
                _blogPosts.Remove(postToRemove);
            }
        }

        private bool ContainsCategoryInList(Category containedCategory)
        {
            var isContains = false;
            foreach (var blogCategory in _blogCategories)
            {
                if (blogCategory.CategoryName == containedCategory.CategoryName)
                {
                    isContains = true;
                }
            }

            return isContains;
        }


        private bool ContainsPostInList(Post containedPost)
        {
            var isContains = false;
            foreach (var blogPost in _blogPosts)
            {
                if ((blogPost.PostTitle == containedPost.PostTitle) &&
                    (blogPost.PostContent == containedPost.PostContent))
                {
                    isContains = true;
                }
            }

            return isContains;
        }
    }
}