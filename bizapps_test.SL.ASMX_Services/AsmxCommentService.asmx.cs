using System;
using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.ASMX_Services.SoapEntities;
using bizapps_test.SL.ASMX_Services.Interfaces;
using System.Web.Services;
using Ninject;
using System.ServiceModel;
using Ninject.Web.Common.WebHost;
using System.Web;

namespace bizapps_test.SL.ASMX_Services
{
    [WebService(Namespace = "http://comments.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AsmxCommentService : WebService, IAsmxCommentService
    {
        [Inject]
        public ICommentService CommentService { get; set; }

        public AsmxCommentService()
            : this(((NinjectHttpApplication)HttpContext.Current.ApplicationInstance).Kernel.Get<ICommentService>())
        {
        }

        public AsmxCommentService(ICommentService commentService)
        {
            CommentService = commentService;
        }


        [WebMethod]
        public int CreateComment(CommentSoap commentSoap, int postId)
        {
            try
            {
                return CommentService.CreateComment(new CommentDto
                {
                    CommentText = commentSoap.CommentText,
                    UserName = commentSoap.UserName,
                    ParentId = commentSoap.ParentId,
                    PostId = commentSoap.PostId
                }, postId);
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public int UpdateComment(CommentSoap commentSoap)
        {
            try
            {
                return CommentService.UpdateComment(new CommentDto
                {
                    Id = commentSoap.Id,
                    CommentText = commentSoap.CommentText,
                    UserName = commentSoap.UserName,
                    ParentId = commentSoap.ParentId,
                    PostId = commentSoap.PostId
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public int DeleteComment(CommentSoap commentSoap)
        {
            try
            {
                return CommentService.DeleteComment(new CommentDto
                {
                    Id = commentSoap.Id,
                    CommentText = commentSoap.CommentText,
                    UserName = commentSoap.UserName,
                    ParentId = commentSoap.ParentId,
                    PostId = commentSoap.PostId
                });

            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public CommentSoap[] GetIndependentComments(int postId)
        {
            try
            {
                List<CommentSoap> commentSoapList = new List<CommentSoap>();
                IEnumerable<CommentDto> commentDtoList = CommentService.GetIndependentComments(postId);
                foreach (CommentDto commentDto in commentDtoList)
                {
                    commentSoapList.Add(new CommentSoap
                    {
                        Id = commentDto.Id,
                        CommentText = commentDto.CommentText,
                        UserName = commentDto.UserName,
                        ParentId = commentDto.ParentId,
                        PostId = commentDto.PostId,
                        CreationDate = commentDto.CreationDate
                    });
                }
                return commentSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public CommentSoap[] GetCommentAnswers(int commentId)
        {
            try
            {
                List<CommentSoap> commentSoapList = new List<CommentSoap>();
                IEnumerable<CommentDto> commentDtoList = CommentService.GetCommentAnswers(commentId);
                foreach (CommentDto commentDto in commentDtoList)
                {
                    commentSoapList.Add(new CommentSoap
                    {
                        Id = commentDto.Id,
                        CommentText = commentDto.CommentText,
                        UserName = commentDto.UserName,
                        ParentId = commentDto.ParentId,
                        PostId = commentDto.PostId,
                        CreationDate = commentDto.CreationDate
                    });
                }
                return commentSoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        [WebMethod]
        public CommentSoap GetComment(int commentId)
        {
            try
            {
                CommentDto commentDto = CommentService.GetComment(commentId);
                return new CommentSoap
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


        [WebMethod]
        public bool HaveMoreThanFourLevels(CommentSoap commentSoap)
        {
            try
            {
                return CommentService.HaveMoreThanFourLevels(new CommentDto
                {
                    Id = commentSoap.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

    }
}
