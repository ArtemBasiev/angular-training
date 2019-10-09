using System;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Infrastructure.Abstract;
using bizapps_test.MVC.Util;
using System.Web.Security;
using bizapps_test.MVC.Filters;

namespace bizapps_test.MVC.Controllers
{
    public class AccountController : Controller
    {
        [Ninject.Inject]
        public IBlogUserService BlogUserService { get; set; }

        [Ninject.Inject]
        public IAuthProvider AuthProvider { get; set; }


        [CustomExceptionHandler]
        [ChildActionOnly]
        [HttpGet]
        public PartialViewResult LoginForm()
        {
            BlogUserViewModel userViewModel = new BlogUserViewModel();

            if (!string.IsNullOrEmpty(AuthProvider.GetCurrentUser()))
            {
                userViewModel.UserName = AuthProvider.GetCurrentUser();
                return PartialView(userViewModel);
            }
            else
            {
                return PartialView();
            }
        }


        [CustomExceptionHandler]
        [HandleError]
        [ChildActionOnly]
        [HttpGet]
        public PartialViewResult AdministrationPanel()
        {
            return PartialView();
        }


        [CustomExceptionHandler]
        [HttpPost]
        public RedirectToRouteResult Login(BlogUserViewModel blogUser)
        {
            if (ModelState.IsValid)
            {
                if (AuthProvider.Authenticate(blogUser.UserName, blogUser.UserPassword))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [CustomExceptionHandler]
        public RedirectToRouteResult SignOut()
        {
            AuthProvider.SignOut();
            return RedirectToAction("Index", "Home");
        }



        [CustomExceptionHandler]
        public ViewResult SignIn()
        {
            return View();
        }



        [CustomExceptionHandler]
        [HttpPost]
        public ActionResult SignIn(BlogUserViewModel blogUserViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BlogUserService.CreateBlogUser(blogUserViewModel);
                    return RedirectToAction("Index", "Home");
                }
                catch(ApplicationException)
                {
                    return View();
                }            
            }
            return View();
        }



        public ViewResult ChangePassword()
        {
            return View();
        }



        [CustomExceptionHandler]
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BlogUserService.ChangePassword(new BlogUserViewModel
                    {
                        UserName = AuthProvider.GetCurrentUser(),
                        UserPassword = changePasswordViewModel.UserPassword,
                    });
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }
    }
}