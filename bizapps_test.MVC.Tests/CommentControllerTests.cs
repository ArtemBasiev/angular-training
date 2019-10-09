using System;
using System.Collections.Generic;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Controllers;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using bizapps_test.MVC.Infrastructure.Abstract;

namespace bizapps_test.MVC.Tests
{
    [TestClass]
    public class CommentControllerTests
    {
        public CommentController Controller { get; set; }


        [TestInitialize]
        public void SetTargetProperty()
        {
            Controller = new CommentController();
        }

        [TestMethod]
        public void Index_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id => id.GetCurrentUser()).Returns("currentuser");
            Controller.AuthProvider = authProvider.Object;

            PartialViewResult result = Controller.Index(new PostViewModel{Id = 1}) as PartialViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void CommentList_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            int expectedCommentId = 1;
            ICommentService commentService = Mock.Of<ICommentService>(id => id.GetIndependentComments(It.IsAny<int>()) == new List<CommentViewModel>
            {
                new CommentViewModel
                {
                    Id = expectedCommentId,
                    CreationDate = DateTime.Now.ToString("D")
                }
            });
            Controller.CommentService = commentService;
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id => id.GetCurrentUser()).Returns("currentuser");
            Controller.AuthProvider = authProvider.Object;

            PartialViewResult result = Controller.CommentList(new CommentViewModel()) as PartialViewResult;
            var returnedCommentViewModels = (List<CommentViewModel>)result.Model;

            Assert.AreEqual(expectedCommentId, returnedCommentViewModels[0].Id);
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CommentList_CommentServiceThrowsException_CatchApplicationException()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.GetIndependentComments(It.IsAny<int>())).Throws(new ApplicationException());
            Controller.CommentService = commentService.Object;

            Controller.CommentList(new CommentViewModel());
        }


        [TestMethod]
        public void CommentChildList_ReturningView_ReturnDefaultViewWithNotNullModel()
        {
            int expectedCommentId = 1;
            Mock<ICommentService> commentService = new Mock<ICommentService>();
            commentService.Setup(id => id.GetCommentAnswers(It.IsAny<int>())).Returns(new List<CommentViewModel>
            {
                new CommentViewModel
                {
                    Id = expectedCommentId,
                    CreationDate = DateTime.Now.ToString("D")
                }
            });
            commentService.Setup(id => id.HaveMoreThanFourLevels(It.IsAny<CommentViewModel>())).Returns(It.IsAny<bool>());
            Controller.CommentService = commentService.Object;
            Mock<IAuthProvider> authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(id => id.GetCurrentUser()).Returns("currentuser");
            Controller.AuthProvider = authProvider.Object;

            PartialViewResult result = Controller.CommentChildList(new CommentViewModel()) as PartialViewResult;
            var returnedCommentViewModels = (List<CommentViewModel>)result.Model;

            Assert.AreEqual(expectedCommentId, returnedCommentViewModels[0].Id);
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CommentChildList_CommentServiceThrowException_CatchApplicationException()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.HaveMoreThanFourLevels(It.IsAny<CommentViewModel>())).Throws(new ApplicationException());
            Controller.CommentService = commentService.Object;

            Controller.CommentChildList(new CommentViewModel());
        }


        [TestMethod]
        public void Comment__ReturningView_ReturnDefaultView()
        {
            PartialViewResult result = Controller.Comment(new CommentViewModel()) as PartialViewResult;

            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void EditComment__ReturningView_ReturnDefaultView()
        {
            PartialViewResult result = Controller.EditComment(new CommentViewModel()) as PartialViewResult;

            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void ReplyComment_ReturningView_ReturnDefaultView()
        {
            PartialViewResult result = Controller.ReplyComment(new CommentViewModel()) as PartialViewResult;

            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void CreateComment_CreatingComment_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.CreateComment(It.IsAny<CommentViewModel>(), It.IsAny<int>())).Returns(1);
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.CreateComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateComment_InvalidModel_RedirectToRouteCommentIndex()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());

            RedirectToRouteResult result = Controller.CreateComment(new CommentViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateComment_CommentServiceThrowsException_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.CreateComment(It.IsAny<CommentViewModel>(), It.IsAny<int>())).Throws(new ApplicationException());
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.CreateComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void ChangeComment_ChangingComment_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.UpdateComment(It.IsAny<CommentViewModel>())).Returns(1);
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.ChangeComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ChangeComment_InvalidModel_RedirectToRouteCommentIndex()
        {
            Controller.ModelState.AddModelError("notvalid", new NullReferenceException());
            RedirectToRouteResult result = Controller.ChangeComment(new CommentViewModel()) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ChangeComment_CommentServiceThrowsException_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.UpdateComment(It.IsAny<CommentViewModel>())).Throws(new ApplicationException());
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.ChangeComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteComment_DeletingComment_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.DeleteComment(It.IsAny<CommentViewModel>())).Returns(1);
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.DeleteComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void DeleteComment_CommentServiceThrowsException_RedirectToRouteCommentIndex()
        {
            Mock<ICommentService> commentService = new Mock<ICommentService>(MockBehavior.Strict);
            commentService.Setup(id => id.DeleteComment(It.IsAny<CommentViewModel>())).Throws(new ApplicationException());
            Controller.CommentService = commentService.Object;

            RedirectToRouteResult result = Controller.DeleteComment(new CommentViewModel
            {
                ActiveUser = It.IsAny<string>()
            }) as RedirectToRouteResult;

            Assert.AreEqual("Comment", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
