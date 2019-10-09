using System;
using System.Collections.Generic;
using System.Web.Http;
using bizapps_test.BLL.DTO;
using Ninject;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Attributes;

namespace bizapps_test.SL.API_Services.Controllers
{
    [RoutePrefix("comment")]
    public class CommentController : ApiController
    {
        [Inject]
        public ICommentService CommentService { get; set; }


        [Route("createcomment")]
        [CustomAuthorize]
        [HttpPost]
        public bool CreateComment([FromBody]CommentDto commentDto)
        {
            try
            {
                commentDto.CreationDate = DateTime.Now;
                var result = CommentService.CreateComment(commentDto);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }


        [Route("updatecomment")]
        [CustomAuthorize]
        [HttpPost]
        public bool UpdateComment([FromBody]CommentDto commentDto)
        {
            try
            {
                commentDto.CreationDate = DateTime.Now;
                var result = CommentService.UpdateComment(commentDto);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
           
        }


        [Route("deletecomment")]
        [CustomAuthorize]
        [HttpPost]
        public bool DeleteComment([FromBody]CommentDto commentDto)
        {
            try
            {
                var result = CommentService.DeleteComment(commentDto);
                if (result == AnswerStatus.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }


        [Route("getpostcomments/{postId}")]
        [Authorize]
        [HttpGet]
        public IEnumerable<CommentDto> GetPostComments(int postId)
        {
            try
            {
                var result = CommentService.GetPostComments(postId);
                if (result.Status == AnswerStatus.Successfull)
                {
                    return result.ReceivedEntity;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
        }



        [Route("getcomment/{commentId}")]
        [HttpGet]
        public CommentDto GetComment(int commentId)
        {
            try
            {
                var result = CommentService.GetCommentById(commentId);
                if (result.Status == AnswerStatus.Successfull)
                {
                    return result.ReceivedEntity;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
        }

    }
}
