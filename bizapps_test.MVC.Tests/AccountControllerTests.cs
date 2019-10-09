using System;
using System.Collections.Generic;
using System.Web;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using bizapps_test.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using bizapps_test.MVC.Infrastructure.Abstract;

namespace bizapps_test.MVC.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        public AccountController Controller { get; set; }

        [TestInitialize]
        public void SetTargetProperty()
        {
            Controller = new AccountController();
        }


        [TestMethod]
        public void LoginForm_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            IAuthProvider authProvider = Mock.Of<IAuthProvider>(id => id.GetCurrentUser() == "currentuser");
            Controller.AuthProvider = authProvider;

            PartialViewResult result = Controller.LoginForm();

            Assert.IsNotNull(result.Model);
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void LoginForm_ReturningView_ReturnDefaulViewWithNullModel()
        {
            IAuthProvider authProvider = Mock.Of<IAuthProvider>(id => id.GetCurrentUser() == "");
            Controller.AuthProvider = authProvider;

            PartialViewResult result = Controller.LoginForm();

            Assert.IsNull(result.Model);
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void AdministrationPanel_ReturningView_ReturnDefaultView()
        {
            PartialViewResult result = Controller.AdministrationPanel();

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void Login_TryingToLogIn_RedirectToHomePage()
        {
            IAuthProvider authProvider = Mock.Of<IAuthProvider>(id => id.Authenticate(It.IsAny<string>(), It.IsAny<string>()) == true);
            Controller.AuthProvider = authProvider;

            RedirectToRouteResult result = Controller.Login(new BlogUserViewModel());

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Login_ModelIsInvalid_RedirectToHomePage()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());

            RedirectToRouteResult result = Controller.Login(new BlogUserViewModel());

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Login_AuthFailed_RedirectToHomePage()
        {
            IAuthProvider authProvider = Mock.Of<IAuthProvider>(id => id.Authenticate(It.IsAny<string>(), It.IsAny<string>()) == false);
            Controller.AuthProvider = authProvider;

            RedirectToRouteResult result = Controller.Login(new BlogUserViewModel());

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void SignOut_TryingToSignOut_RedirectToHomePage()
        {
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id=>id.SignOut()).Verifiable();
            Controller.AuthProvider = authProvider.Object;

            RedirectToRouteResult result = Controller.SignOut();

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void SignIn_ReturningView_ReturnDefaultView()
        {
            ViewResult result = Controller.SignIn();

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void SignIn_CreateUser_RedirectToHomePage()
        {
            Mock<IBlogUserService> blogUserService = new Mock<IBlogUserService>();
            blogUserService.Setup(id => id.CreateBlogUser(It.IsAny<BlogUserViewModel>())).Returns(1);
            Controller.BlogUserService = blogUserService.Object;

            RedirectToRouteResult result = Controller.SignIn(new BlogUserViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void SignIn_ModelIsInvalid_ReturnDefaultView()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());

            ViewResult result = Controller.SignIn(new BlogUserViewModel()) as ViewResult;

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void SignIn_BlogUserServiceThrowsException_ReturnDefaultView()
        {
            Mock<IBlogUserService> blogUserService = new Mock<IBlogUserService>();
            blogUserService.Setup(id => id.CreateBlogUser(It.IsAny<BlogUserViewModel>())).Throws(new ApplicationException());
            Controller.BlogUserService = blogUserService.Object;

            ViewResult result = Controller.SignIn(new BlogUserViewModel()) as ViewResult;

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void ChangePassword_ReturningView_ReturnDefaultView()
        {
            ViewResult result = Controller.ChangePassword();

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void ChangePassword_ChangingPassword_RedirectToHomePage()
        {
            Mock<IBlogUserService> blogUserService = new Mock<IBlogUserService>();
            blogUserService.Setup(id => id.ChangePassword(It.IsAny<BlogUserViewModel>())).Returns(1);
            Controller.BlogUserService = blogUserService.Object;
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id => id.GetCurrentUser()).Returns("currentuser");           
            Controller.AuthProvider = authProvider.Object;

            RedirectToRouteResult result = Controller.ChangePassword(new ChangePasswordViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void ChangePassword_ModelIsInvalid_ReturnDefaultView()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());

            ViewResult result = Controller.ChangePassword(new ChangePasswordViewModel()) as ViewResult;

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void ChangePassword_BlogUserServiceThrowsException_ReturnDefaultView()
        {
            Mock<IBlogUserService> blogUserService = new Mock<IBlogUserService>();
            blogUserService.Setup(id => id.ChangePassword(It.IsAny<BlogUserViewModel>())).Throws(new ApplicationException());
            Controller.BlogUserService = blogUserService.Object;
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id => id.GetCurrentUser()).Returns("currentuser");
            Controller.AuthProvider = authProvider.Object;

            ViewResult result = Controller.ChangePassword(new ChangePasswordViewModel()) as ViewResult;

            Assert.AreEqual("", result.ViewName);
        }
    }
}
