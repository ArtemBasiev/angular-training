using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.WcfCommentServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.WcfServices
{
    public class WcfCommentService: ICommentService
    {
        public WcfCommentServiceClient CommentServiceClient { get; set; }

        public WcfCommentService()
        {
            CommentServiceClient = new WcfCommentServiceClient();
        }

        public int CreateComment(CommentViewModel commentViewModel, int postId)
        {
            try
            {
                return CommentServiceClient.CreateComment(new CommentDC
                {
                    CommentText = commentViewModel.CommentText,
                    UserName = commentViewModel.UserName,
                    ParentId = commentViewModel.ParentId,
                    PostId = commentViewModel.PostId
                }, postId);
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int UpdateComment(CommentViewModel commentViewModel)
        {
            try
            {
                return CommentServiceClient.UpdateComment(new CommentDC
                {
                    Id = commentViewModel.Id,
                    CommentText = commentViewModel.CommentText,
                    UserName = commentViewModel.UserName,
                    ParentId = commentViewModel.ParentId,
                    PostId = commentViewModel.PostId
                });
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public int DeleteComment(CommentViewModel commentViewModel)
        {
            try
            {
                return CommentServiceClient.DeleteComment(new CommentDC
                {
                    Id = commentViewModel.Id,
                    CommentText = commentViewModel.CommentText,
                    UserName = commentViewModel.UserName,
                    ParentId = commentViewModel.ParentId,
                    PostId = commentViewModel.PostId
                });

            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CommentViewModel> GetIndependentComments(int postId)
        {
            try
            {
                List<CommentViewModel> commentViewModelList = new List<CommentViewModel>();
                IEnumerable<CommentDC> commentDCList = CommentServiceClient.GetIndependentComments(postId);
                foreach (CommentDC commentDC in commentDCList)
                {
                    commentViewModelList.Add(new CommentViewModel
                    {
                        Id = commentDC.Id,
                        CommentText = commentDC.CommentText,
                        UserName = commentDC.UserName,
                        ParentId = commentDC.ParentId,
                        PostId = commentDC.PostId,
                        CreationDate = commentDC.CreationDate.ToString()
                    });
                }
                return commentViewModelList;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public IEnumerable<CommentViewModel> GetCommentAnswers(int commentId)
        {
            try
            {
                List<CommentViewModel> commentViewModelList = new List<CommentViewModel>();
                IEnumerable<CommentDC> commentDCList = CommentServiceClient.GetCommentAnswers(commentId);
                foreach (CommentDC commentDC in commentDCList)
                {
                    commentViewModelList.Add(new CommentViewModel
                    {
                        Id = commentDC.Id,
                        CommentText = commentDC.CommentText,
                        UserName = commentDC.UserName,
                        ParentId = commentDC.ParentId,
                        PostId = commentDC.PostId,
                        CreationDate = commentDC.CreationDate.ToString()
                    });
                }
                return commentViewModelList;
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public CommentViewModel GetComment(int commentId)
        {
            try
            {
                CommentDC commentDC = CommentServiceClient.GetComment(commentId);
                return new CommentViewModel
                {
                    Id = commentDC.Id,
                    CommentText = commentDC.CommentText,
                    UserName = commentDC.UserName,
                    ParentId = commentDC.ParentId,
                    PostId = commentDC.PostId,
                    CreationDate = commentDC.CreationDate.ToString()
                };
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public bool HaveMoreThanFourLevels(CommentViewModel commentViewModel)
        {
            try
            {
                return CommentServiceClient.HaveMoreThanFourLevels(new CommentDC
                {
                    Id = commentViewModel.Id
                });
            }
            catch (FaultException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}