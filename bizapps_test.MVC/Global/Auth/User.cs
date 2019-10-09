using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizapps_test.MVC.Global.Auth
{
    public class User
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public bool IsPersistent { get; private set; }

        public User(string login, string password, bool isPersistent)
        {
            Login = login;
            Password = password;
            IsPersistent = isPersistent;
        }

        public User(string login)
        {
            Login = login;
        }
    }
}