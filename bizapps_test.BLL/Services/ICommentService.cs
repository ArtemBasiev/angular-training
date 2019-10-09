using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.DAL.Repositories;

namespace bizapps_test.BLL.Services
{
    public interface ICommentService
    {
        AnswerStatus CreateComment(CommentDto commentDTO);

        AnswerStatus UpdateComment(CommentDto commentDTO);

        AnswerStatus DeleteComment(CommentDto commentDTO);

        ServiceAnswer<CommentDto> GetCommentById(int commentId);

        ServiceAnswer<IEnumerable<CommentDto>> GetPostComments(int postId);
    }
}