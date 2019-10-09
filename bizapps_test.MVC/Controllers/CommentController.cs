using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using bizapps_test.MVC.Filters;
using bizapps_test.MVC.Models;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Util;
using bizapps_test.MVC.Infrastructure.Abstract;

namespace bizapps_test.MVC.Controllers
{
    public class CommentController : Controller
    {
        [Ninject.Inject]
        public ICommentService CommentService { get; set; }

        [Ninject.Inject]
        public IAuthProvider AuthProvider { get; set; }


        [CustomExceptionHandler]
        public ActionResult Index(PostViewModel postViewModel)
        {

            CommentViewModel commentViewModel = new CommentViewModel
            {
                PostId = postViewModel.Id
            };

            if(!string.IsNullOrEmpty(AuthProvider.GetCurrentUser()))
            {
                commentViewModel.ActiveUser = AuthProvider.GetCurrentUser(); 
            }

            return PartialView(commentViewModel);
        }


        [CustomExceptionHandler]
        public ActionResult CommentList(CommentViewModel commentViewModel)
        {
            List<CommentViewModel> commentViewModels = GetCommentList(CommentService.GetIndependentComments(commentViewModel.PostId));
            return PartialView(commentViewModels);
        }


        [CustomExceptionHandler]
        public ActionResult CommentChildList(CommentViewModel commentViewModel)
        {
            ViewData["HaveMoreThanFourLevels"] = CommentService.HaveMoreThanFourLevels(commentViewModel);
            List<CommentViewModel> commentViewModels = GetCommentList(CommentService.GetCommentAnswers(commentViewModel.Id));
            return PartialView(commentViewModels);
        }


        private List<CommentViewModel> GetCommentList(IEnumerable<CommentViewModel> commentList)
        {
            foreach (CommentViewModel commentViewModel in commentList)
            {
                commentViewModel.CreationDate = CommentDateGenerator.GetDateString(Convert.ToDateTime(commentViewModel.CreationDate));
                if (!string.IsNullOrEmpty(AuthProvider.GetCurrentUser()))
                {
                    commentViewModel.ActiveUser = AuthProvider.GetCurrentUser();
                }
            }
            return commentList.ToList();
        }



        [CustomExceptionHandler]
        public ActionResult Comment(CommentViewModel commentViewModel)
        {
            return PartialView(commentViewModel);
        }



        [CustomExceptionHandler]
        public ActionResult EditComment(CommentViewModel commentViewModel)
        {
            return PartialView(commentViewModel);
        }



        [CustomExceptionHandler]
        public ActionResult ReplyComment(CommentViewModel commentViewModel)
        {
            return PartialView(commentViewModel);
        }



        [CustomExceptionHandler]
        [Authorize]
        [HttpPost]
        public ActionResult CreateComment(CommentViewModel commentViewModel)
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        commentViewModel.UserName = commentViewModel.ActiveUser;
                        CommentService.CreateComment(commentViewModel, commentViewModel.PostId);

                        commentViewModel.CommentText = null;
                        return RedirectToAction("Index", "Comment", new PostViewModel{Id = commentViewModel.PostId});
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                }
        }



        [CustomExceptionHandler]
        [Authorize]
        [HttpPost]
        public ActionResult ChangeComment(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CommentService.UpdateComment(commentViewModel);

                    commentViewModel.CommentText = null;

                    return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                }
            }
            else
            {
                return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
            }
        }



        [CustomExceptionHandler]
        [Authorize]
        [HttpPost]
        public ActionResult DeleteComment(CommentViewModel commentViewModel)
        {
                try
                {
                    CommentService.DeleteComment(commentViewModel);

                    commentViewModel.CommentText = null;
                    return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Comment", new PostViewModel { Id = commentViewModel.PostId });
                }
        }
       
    }
}