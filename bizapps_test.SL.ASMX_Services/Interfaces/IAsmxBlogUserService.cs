using System.Collections.Generic;
using bizapps_test.SL.ASMX_Services.SoapEntities;

namespace bizapps_test.SL.ASMX_Services.Interfaces
{
    public interface IAsmxBlogUserService
    {
        int CreateBlogUser(BlogUserSoap bloguserSoap);

        int UpdateBlogUser(BlogUserSoap bloguserSoap);

        int DeleteBlogUser(BlogUserSoap bloguserSoap);

        int ChangePassword(BlogUserSoap blogUserSoap);

        BlogUserSoap[] GetAllUsers();

        BlogUserSoap GetBlogUserById(int userId);

        BlogUserSoap GetBlogUserByName(string userName);


        BlogUserSoap GetBlogUserNameAndPassword(BlogUserSoap incomingUser);

        int GetAdminPermission(string userName);
    }
}
