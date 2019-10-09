using System;

namespace bizapps_test.Domain.Models
{
    public class Category 
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when categoryName parameter is empty
        /// </exception>
        public Category(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException("CategoryName cannot be empty!");
            CategoryName = categoryName;
        }

        public int Id { get; internal set; }

        public string CategoryName { get; private set; }

        public Blog RelatedTo { get; internal set; }
    }
}