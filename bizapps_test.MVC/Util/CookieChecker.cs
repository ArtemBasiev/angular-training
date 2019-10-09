using System.Web;


namespace bizapps_test.MVC.Util
{
    public static class CookieChecker
    {
        public static bool CookieIsActual(HttpCookie login, HttpCookie sign)
        {
            if (login != null && sign != null)
            {
                if (sign.Value == SignGenerator.GetSign(login.Value + "byte"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool HaveAdminPermission(HttpCookie perm)
        {
            if (perm!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}