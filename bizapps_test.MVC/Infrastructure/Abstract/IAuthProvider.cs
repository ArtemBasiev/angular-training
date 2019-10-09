
namespace bizapps_test.MVC.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string userName, string password);

        string GetCurrentUser();

        void SignOut();
    }
}
