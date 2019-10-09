using System;
using System.Collections.Generic;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Controllers;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;


namespace bizapps_test.MVC.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        public HomeController Controller { get; set; }


        [TestInitialize]
        public void SetTargetProperty()
        {
            Controller = new HomeController();
        }

        [TestMethod]
        public void Index_ReturnViewWithNullCategoryId_ReturnDefaultViewWithNotNullModel()
        {
            int expectedPostId = 1;
            ICategoryService categoryService = Mock.Of<ICategoryService>(id =>
                id.GetPostCategories(It.IsAny<int>()) == new List<CategoryViewModel>());
            IPostService postService = Mock.Of<IPostService>(id => id.GetUserPostsByUserName(It.IsAny<string>()) == new List<PostViewModel>{new PostViewModel
            {
                Id=expectedPostId,
                PostImage = "image",
                CreationDate = DateTime.Now.ToString("D")
            }});
            Controller.PostService = postService;
            Controller.CategoryService = categoryService;

            ViewResult result = Controller.Index(null);
            var returnedPostViewModelList = (PostPagedListViewModel)result.Model;

            Assert.AreEqual(expectedPostId, returnedPostViewModelList.PostList[0].Id);
            Assert.AreEqual("", result.ViewName);           
        }

        [TestMethod]
        public void Index_ReturningViewWithSetCategory_ReturnDefaultViewWithNotNullModel()
        {
            int expectedPostId = 1;
            ICategoryService categoryService = Mock.Of<ICategoryService>(id =>
                id.GetPostCategories(It.IsAny<int>()) == new List<CategoryViewModel>());
            IPostService postService = Mock.Of<IPostService>(id => id.GetPostsByUserNameAndCategory(It.IsAny<string>(), It.IsAny<int>()) == new List<PostViewModel>{new PostViewModel
            {
                Id=expectedPostId,
                PostImage = "image",
                CreationDate = DateTime.Now.ToString("D")
            }});
            Controller.PostService = postService;
            Controller.CategoryService = categoryService;

            ViewResult result = Controller.Index("1");
            var returnedPostViewModelList = (PostPagedListViewModel) result.Model;

            Assert.AreEqual(expectedPostId, returnedPostViewModelList.PostList[0].Id);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Index_ReturningViewWithoutCategories_ReturnDefaultViewWithNotNullModel()
        {
            int expectedPostId = 1;
            ICategoryService categoryService = Mock.Of<ICategoryService>(id =>
                id.GetPostCategories(It.IsAny<int>()) == new List<CategoryViewModel>());
            IPostService postService = Mock.Of<IPostService>(id => id.GetPostsByUserNameWithoutCategory(It.IsAny<string>()) == new List<PostViewModel>{new PostViewModel
            {
                Id=expectedPostId,
                PostImage = "image",
                CreationDate = DateTime.Now.ToString("D")
            }});
            Controller.PostService = postService;
            Controller.CategoryService = categoryService;

            ViewResult result = Controller.Index("withoutcategory");
            var returnedPostViewModelList = (PostPagedListViewModel)result.Model;

            Assert.AreEqual(expectedPostId, returnedPostViewModelList.PostList[0].Id);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Index_PostServiceThrowsException_CatchApplicationException()
        {
            Mock<IPostService> postService = new Mock<IPostService>(MockBehavior.Strict);
            postService.Setup(id => id.GetUserPostsByUserName(It.IsAny<string>())).Throws(new ApplicationException());
            Controller.PostService = postService.Object;

            Controller.Index(null);
        }


        [TestMethod]
        public void CategorySideBar_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            int expectedCategoryId = 1;
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.GetBlogCategories(It.IsAny<string>()) == new List<CategoryViewModel>{new CategoryViewModel
            {
                Id = expectedCategoryId
            }});
            Controller.CategoryService = categoryService;

            PartialViewResult result = Controller.CategorySideBar();
            var returnedCategoryViewModelList = (List<CategoryViewModel>)result.Model;

            Assert.AreEqual(expectedCategoryId, returnedCategoryViewModelList[0].Id);
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CategorySideBar_CategoryServiceThrowsException_CatchApplicationException()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryService.Setup(id => id.GetBlogCategories(It.IsAny<string>())).Throws(new ApplicationException());
            Controller.CategoryService = categoryService.Object;

            Controller.CategorySideBar();
        }


        [TestMethod]
        public void CategoryUpdateList_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            int expectedCategoryId = 1;
            ICategoryService categoryService = Mock.Of<ICategoryService>(id => id.GetAllCategories() == new List<CategoryViewModel>{new CategoryViewModel
            {
                Id = expectedCategoryId
            }});
            Controller.CategoryService = categoryService;

            PartialViewResult result = Controller.CategoryUpdateList();
            var returnedCategoryViewModelList = (List<CategoryViewModel>)result.Model;

            Assert.AreEqual(expectedCategoryId, returnedCategoryViewModelList[0].Id);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CategoryUpdateList_CategoryServiceThrowsException_CatchApplicationException()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryService.Setup(id => id.GetAllCategories()).Throws(new ApplicationException());
            Controller.CategoryService = categoryService.Object;

            Controller.CategoryUpdateList();
        }

    }
}
