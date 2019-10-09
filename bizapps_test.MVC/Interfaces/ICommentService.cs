using System.Collections.Generic;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Interfaces
{
    public interface ICommentService
    {
        int CreateComment(CommentViewModel commentDto, int postId);

        int UpdateComment(CommentViewModel commentDto);

        int DeleteComment(CommentViewModel commentDto);

        IEnumerable<CommentViewModel> GetIndependentComments(int postId);

        IEnumerable<CommentViewModel> GetCommentAnswers(int commentId);

        CommentViewModel GetComment(int commentId);

        bool HaveMoreThanFourLevels(CommentViewModel commentDto);
    }
}
