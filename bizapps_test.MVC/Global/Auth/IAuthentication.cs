using System.Security.Principal;
using System.Web;



namespace bizapps_test.MVC.Global.Auth
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        User Login(string login, string password, bool isPersistent);

        //User Login(string login);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
