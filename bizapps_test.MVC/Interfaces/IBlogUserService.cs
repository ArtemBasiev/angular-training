using System.Collections.Generic;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Interfaces
{
    public interface IBlogUserService
    {
        int CreateBlogUser(BlogUserViewModel bloguserDto);

        int UpdateBlogUser(BlogUserViewModel bloguserDto);

        int DeleteBlogUser(BlogUserViewModel bloguserDto);

        int ChangePassword(BlogUserViewModel blogUserDto);

        IEnumerable<BlogUserViewModel> GetAllUsers();

        BlogUserViewModel GetBlogUserById(int userId);

        BlogUserViewModel GetBlogUserByName(string userName);

        BlogUserViewModel GetBlogUserNameAndPassword(BlogUserViewModel incomingUser);

        int GetAdminPermission(string userName);
    }
}
