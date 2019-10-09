using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using bizapps_test.BLL.Interfaces;

namespace bizapps_test.MVC.Global.Auth
{
    public class UserProvider : IPrincipal
    {
        private UserIndentity UserIdentity { get; set; }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return UserIdentity; }
        }

        public bool IsInRole(string role)
        {
            //if (UserIdentity.User == null)
            //{
            //    return false;
            //}
            //return UserIdentity.User.InRoles(role);
            return true;
        }

        #endregion


        public UserProvider(string name, IBlogUserService blogUserService)
        {
            UserIdentity = new UserIndentity();
            UserIdentity.Init(name, blogUserService);
        }


        public override string ToString()
        {
            return UserIdentity.Name;
        }
    }
}