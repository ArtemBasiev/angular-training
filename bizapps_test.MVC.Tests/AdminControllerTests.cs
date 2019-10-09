using System;
using System.Collections.Generic;
using System.Web;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Controllers;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace bizapps_test.MVC.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        public AdminController Controller { get; set; }


        [TestInitialize]
        public void SetTargetProperty()
        {
            Controller = new AdminController();         
        }

        [TestMethod]
        public void Post_ReturningView_ReturnDefaultViewAndModelWithExpectedParameters()
        {
            int expectedPostId = 1;
            string expectedPostTitle = "title";
            string expectedPostBody = "body";
            string expectedPostImage = "image";
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.GetPostCategories(It.IsAny<int>()) == new List<CategoryViewModel>());
            IPostService postService = Mock.Of<IPostService>(id => id.GetPost(It.IsAny<int>()) == new PostViewModel
            {
                Id = expectedPostId,
                Title = expectedPostTitle,
                Body = expectedPostBody,
                PostImage = expectedPostImage
            });
            Controller.PostService = postService;
            Controller.CategoryService = categoryService;

            ViewResult result = Controller.Post(new PostViewModel()) as ViewResult;
            var returnedPostViewModel = (PostViewMoreModel)result.Model;

            Assert.AreEqual(expectedPostId, returnedPostViewModel.Post.Id);
            Assert.AreEqual(HttpUtility.HtmlDecode(expectedPostBody), returnedPostViewModel.Post.Body);
            Assert.AreEqual(expectedPostTitle, returnedPostViewModel.Post.Title);
            Assert.IsTrue(returnedPostViewModel.Post.PostImage.Contains(expectedPostImage));
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Post_PostServiceThrowsException_CatchApplicationException()
        {
            Mock<IPostService> postService = new Mock<IPostService>(MockBehavior.Strict);
            postService.Setup(id => id.GetPost(It.IsAny<int>())).Throws(new ApplicationException());
            Controller.PostService = postService.Object;

            Controller.Post(new PostViewModel());
        }


        [TestMethod]
        public void CreateUpdatePost_ReturningViewToCreatePost_ReturningDefaultViewWithNotNullModel()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            categoryService.Setup(m => m.GetAllCategories()).Returns(new List<CategoryViewModel>());
            Controller.CategoryService = categoryService.Object;

            PartialViewResult result = Controller.CreateUpdatePost() as PartialViewResult;
            var returnedPostViewModel = (PostViewModel)result.Model;

            Assert.AreEqual(0, returnedPostViewModel.Id);
            Assert.IsNull(returnedPostViewModel.Body);
            Assert.IsNull(returnedPostViewModel.Title);
            Assert.IsNull(returnedPostViewModel.PostImage);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void CreateUpdatePost_ReturningViewToUpdatePost_ReturningDefaultViewAndModelWithExpectedParameters()
        {
            int expectedPostId = 1;
            string expectedPostTitle = "title";
            string expectedPostBody = "body";
            string expectedPostImage = "image";
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            categoryService.Setup(m => m.GetAllCategories()).Returns(new List<CategoryViewModel>());
            categoryService.Setup(m => m.GetPostCategories(It.IsAny<int>())).Returns(new List<CategoryViewModel>());
            Controller.CategoryService = categoryService.Object;

            PartialViewResult result = Controller.CreateUpdatePost(new PostViewModel
            {
                Id = expectedPostId,
                Title = expectedPostTitle,
                Body = expectedPostBody,
                PostImage = expectedPostImage
            }) as PartialViewResult;
            var returnedPostViewModel = (PostViewModel)result.Model;

            Assert.AreEqual(expectedPostId, returnedPostViewModel.Id);
            Assert.AreEqual(expectedPostBody, returnedPostViewModel.Body);
            Assert.AreEqual(expectedPostTitle, returnedPostViewModel.Title);
            Assert.AreEqual(expectedPostImage, returnedPostViewModel.PostImage);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CreateUpdatePost_CategoryServiceThrowsException_CatchApplicationException()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryService.Setup(id => id.GetAllCategories()).Throws(new ApplicationException());
            Controller.CategoryService = categoryService.Object;

            Controller.CreateUpdatePost();
        }


        [TestMethod]
        public void CreateUpdatePostDo_CreatingPost_RedirectToHomePage()
        {
            string expectedPostTitle = "title";
            string expectedPostBody = "body";
            string expectedPostImage = "image";
            IPostService postService = Mock.Of<IPostService>(id => id.CreatePost(It.IsAny<PostViewModel>(), It.IsAny<int>(), It.IsAny<List<CategoryViewModel>>()) == 1);
            Controller.PostService = postService;

            RedirectToRouteResult result = Controller.CreateUpdatePostDo(new PostViewModel
            {
                Title = expectedPostTitle,
                Body = expectedPostBody,
                PostImage = expectedPostImage,
                Categories = new CategoryViewModel[0]
            }) as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdatePostDo_UpdatingPost_RedirectToHomePage()
        {
            int expectedPostId = 1;
            string expectedPostTitle = "title";
            string expectedPostBody = "body";
            string expectedPostImage = "image";
            IPostService postService = Mock.Of<IPostService>(id => id.UpdatePost(It.IsAny<PostViewModel>(), It.IsAny<List<CategoryViewModel>>()) == 1);
            Controller.PostService = postService;

            RedirectToRouteResult result = Controller.CreateUpdatePostDo(new PostViewModel
            {
                Id = 1,
                Title = expectedPostTitle,
                Body = expectedPostBody,
                PostImage = expectedPostImage,
                Categories = new CategoryViewModel[0]
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("Post", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdatePostDo_InvalidModel_RedirectToRouteAdminCreateUpdatePost()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());
            RedirectToRouteResult result = Controller.CreateUpdatePostDo(new PostViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateUpdatePost", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdatePostDo_PostServiceThrowsException_RedirectToRouteAdminCreateUpdatePost()
        {
            Mock<IPostService> postService = new Mock<IPostService>(MockBehavior.Strict);
            postService.Setup(id => id.CreatePost(It.IsAny<PostViewModel>(), It.IsAny<int>(), It.IsAny<List<CategoryViewModel>>())).Throws(new ApplicationException());
            Controller.PostService = postService.Object;

            RedirectToRouteResult result =  Controller.CreateUpdatePostDo(new PostViewModel
            {
                Title = "title",
                Body = "body",
                PostImage = "postImage",
                Categories = new CategoryViewModel[0]
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateUpdatePost", result.RouteValues["action"]);
        }


        [TestMethod]
        public void DeletePost_DeletingPost_RedirectToHomePage()
        {
            Mock<IPostService> postService = new Mock<IPostService>(MockBehavior.Strict);
            postService.Setup(id => id.DeletePost(It.IsAny<PostViewModel>())).Returns(1);
            Controller.PostService = postService.Object;

            RedirectToRouteResult result = Controller.DeletePost(new PostViewModel
            {
               Id = 1
            }) as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeletePost_SetEmptyPostViewModel_RedirectToRouteAdminCreateUpdatePost()
        {
            Mock<IPostService> postService = new Mock<IPostService>();
            postService.Setup(id => id.DeletePost(It.IsAny<PostViewModel>())).Returns(1);
            Controller.PostService = postService.Object;

            RedirectToRouteResult result = Controller.DeletePost(new PostViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateUpdatePost", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeletePost_PostServiceThrowsException_RedirectToRouteAdminCreateUpdatePost()
        {
            Mock<IPostService> postService = new Mock<IPostService>(MockBehavior.Strict);
            postService.Setup(id => id.DeletePost(It.IsAny<PostViewModel>())).Throws(new ApplicationException());
            Controller.PostService = postService.Object;

            RedirectToRouteResult result = Controller.DeletePost(new PostViewModel
            {
                Id = 1
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateUpdatePost", result.RouteValues["action"]);
        }


        [TestMethod]
        public void CreateCategory_ReturningView_ReturnDefaultView()
        {
            PartialViewResult result = Controller.CreateCategory(new CategoryViewModel()) as PartialViewResult;

            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void UpdateDeleteCategory_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            PartialViewResult result = Controller.UpdateDeleteCategory(new CategoryViewModel()) as PartialViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void CreateUpdateCategoryDo_CreatingCategory_RedirectToRouteAdminCreateCategory()
        {
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.CreateCategory(It.IsAny<CategoryViewModel>()) == 1);
            Controller.CategoryService = categoryService;

            RedirectToRouteResult result = Controller.CreateUpdateCategoryDo(new CategoryViewModel
            {
                CategoryName = "category"
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdateCategoryDo_UpdatingCategory_RedirectToRouteAdminCreateCategory()
        {
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.UpdateCategory(It.IsAny<CategoryViewModel>()) == 1);
            Controller.CategoryService = categoryService;

            RedirectToRouteResult result = Controller.CreateUpdateCategoryDo(new CategoryViewModel
            {
                Id = 1,
                CategoryName = "category"
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdateCategoryDo_InvalidModel_RedirectToRouteAdminCreateCategory()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());

            RedirectToRouteResult result = Controller.CreateUpdateCategoryDo(new CategoryViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateUpdateCategoryDo_CategoryServiceThrowsException_RedirectToRouteAdminCreateCategory()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryService.Setup(id => id.CreateCategory(It.IsAny<CategoryViewModel>())).Throws(new ApplicationException());
            Controller.CategoryService = categoryService.Object;

            RedirectToRouteResult result = Controller.CreateUpdateCategoryDo(new CategoryViewModel
            {
                CategoryName = "category"
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteCategory_DeletingCategory__RedirectToRouteAdminCreateCategory()
        {
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.DeleteCategory(It.IsAny<CategoryViewModel>()) == 1);
            Controller.CategoryService = categoryService;

            RedirectToRouteResult result = Controller.DeleteCategory(new CategoryViewModel
            {
                Id = 1
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteCategory_SetEmptyCategoryViewModel_RedirectToRouteAdminCreateCategory()
        {
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.DeleteCategory(It.IsAny<CategoryViewModel>()) == 1);
            Controller.CategoryService = categoryService;

            RedirectToRouteResult result = Controller.DeleteCategory(It.IsAny<CategoryViewModel>()) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteCategory_CategoryServiceThrowsException_RedirectToRouteAdminCreateCategory()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryService.Setup(id => id.DeleteCategory(It.IsAny<CategoryViewModel>())).Throws(new ApplicationException());
            Controller.CategoryService = categoryService.Object;

            RedirectToRouteResult result = Controller.DeleteCategory(new CategoryViewModel
            {
                Id = 1
            }) as RedirectToRouteResult;

            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual("CreateCategory", result.RouteValues["action"]);
        }
    }
}
