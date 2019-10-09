using System;

namespace bizapps_test.Domain.Models
{
    public class BlogUser 
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one of the parameters is empty
        /// </exception>
        /// <param name="userBlog">A blog related with user.</param>
        internal BlogUser(string userName, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName cannot be empty!");
            UserName = userName;

            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userPassword cannot be empty!");
            UserPassword = userPassword;
        }

        public int Id { get; internal set; }

        public string UserName { get; private set; }

        public string UserPassword { get; private set; }

        public Blog UserBlog { get; internal set; }
    }
}