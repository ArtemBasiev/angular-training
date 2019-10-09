using System;
using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.Wcf_Services.DataContracts;
using Ninject;

namespace bizapps_test.SL.Wcf_Services
{
    public class WcfCommentService : IWcfCommentService
    {
        [Inject]
        public ICommentService CommentService { get; set; }

        public int CreateComment(CommentDC commentDC, int postId)
        {
            try
            {
                return CommentService.CreateComment(new CommentDto
                {
                    CommentText = commentDC.CommentText,
                    UserName = commentDC.UserName,
                    ParentId = commentDC.ParentId,
                    PostId = commentDC.PostId
                }, postId);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int UpdateComment(CommentDC commentDC)
        {
            try
            {
                return CommentService.UpdateComment(new CommentDto
                {
                    Id =commentDC.Id,
                    CommentText = commentDC.CommentText,
                    UserName = commentDC.UserName,
                    ParentId = commentDC.ParentId,
                    PostId = commentDC.PostId
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public int DeleteComment(CommentDC commentDC)
        {
            try
            {
                return CommentService.DeleteComment(new CommentDto
                {
                    Id = commentDC.Id,
                    CommentText = commentDC.CommentText,
                    UserName = commentDC.UserName,
                    ParentId = commentDC.ParentId,
                    PostId = commentDC.PostId
                });

            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<CommentDC> GetIndependentComments(int postId)
        {
            try
            {
                List<CommentDC> commentDCList = new List<CommentDC>();
                IEnumerable<CommentDto> commentDtoList = CommentService.GetIndependentComments(postId);
                foreach (CommentDto commentDto in commentDtoList)
                {
                    commentDCList.Add(new CommentDC
                    {
                        Id = commentDto.Id,
                        CommentText = commentDto.CommentText,
                        UserName = commentDto.UserName,
                        ParentId = commentDto.ParentId,
                        PostId = commentDto.PostId,
                        CreationDate = commentDto.CreationDate
                    });
                }
                return commentDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public IEnumerable<CommentDC> GetCommentAnswers(int commentId)
        {
            try
            {
                List<CommentDC> commentDCList = new List<CommentDC>();
                IEnumerable<CommentDto> commentDtoList = CommentService.GetCommentAnswers(commentId);
                foreach (CommentDto commentDto in commentDtoList)
                {
                    commentDCList.Add(new CommentDC
                    {
                        Id = commentDto.Id,
                        CommentText = commentDto.CommentText,
                        UserName = commentDto.UserName,
                        ParentId = commentDto.ParentId,
                        PostId = commentDto.PostId,
                        CreationDate = commentDto.CreationDate
                    });
                }
                return commentDCList;
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public CommentDC GetComment(int commentId)
        {
            try
            {
                CommentDto commentDto = CommentService.GetComment(commentId);
                return new CommentDC
                {
                    Id = commentDto.Id,
                    CommentText = commentDto.CommentText,
                    UserName = commentDto.UserName,
                    ParentId = commentDto.ParentId,
                    PostId = commentDto.PostId,
                    CreationDate = commentDto.CreationDate
                };
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public bool HaveMoreThanFourLevels(CommentDC commentDC)
        {
            try
            {
                return CommentService.HaveMoreThanFourLevels(new CommentDto
                {
                    Id = commentDC.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
