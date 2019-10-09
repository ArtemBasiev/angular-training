using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.MVC.AsmxCommentServiceReference;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Services.AsmxServices
{
    public class AsmxCommentService: ICommentService
    {
        public AsmxCommentServiceSoapClient CommentServiceClient { get; set; }

        public AsmxCommentService()
        {
            CommentServiceClient = new AsmxCommentServiceSoapClient();
        }

        public int CreateComment(CommentViewModel commentViewModel, int postId)
        {
            try
            {
                return CommentServiceClient.CreateComment(new CommentSoap
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
                return CommentServiceClient.UpdateComment(new CommentSoap
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
                return CommentServiceClient.DeleteComment(new CommentSoap
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
                IEnumerable<CommentSoap> commentSoapList = CommentServiceClient.GetIndependentComments(postId);
                foreach (CommentSoap comment in commentSoapList)
                {
                    commentViewModelList.Add(new CommentViewModel
                    {
                        Id = comment.Id,
                        CommentText = comment.CommentText,
                        UserName = comment.UserName,
                        ParentId = comment.ParentId,
                        PostId = comment.PostId,
                        CreationDate = comment.CreationDate.ToString()
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
                IEnumerable<CommentSoap> commentSoapList = CommentServiceClient.GetCommentAnswers(commentId);
                foreach (CommentSoap comment in commentSoapList)
                {
                    commentViewModelList.Add(new CommentViewModel
                    {
                        Id = comment.Id,
                        CommentText = comment.CommentText,
                        UserName = comment.UserName,
                        ParentId = comment.ParentId,
                        PostId = comment.PostId,
                        CreationDate = comment.CreationDate.ToString()
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
                CommentSoap comment = CommentServiceClient.GetComment(commentId);
                return new CommentViewModel
                {
                    Id = comment.Id,
                    CommentText = comment.CommentText,
                    UserName = comment.UserName,
                    ParentId = comment.ParentId,
                    PostId = comment.PostId,
                    CreationDate = comment.CreationDate.ToString()
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
                return CommentServiceClient.HaveMoreThanFourLevels(new CommentSoap
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