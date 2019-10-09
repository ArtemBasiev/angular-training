using bizapps_test.SL.ASMX_Services.SoapEntities;

namespace bizapps_test.SL.ASMX_Services.Interfaces
{
    public interface IAsmxCommentService
    {
        int CreateComment(CommentSoap commentSoap, int postId);

        int UpdateComment(CommentSoap commentSoap);

        int DeleteComment(CommentSoap commentSoap);

        CommentSoap[] GetIndependentComments(int postId);

        CommentSoap[] GetCommentAnswers(int commentId);

        CommentSoap GetComment(int commentId);

        bool HaveMoreThanFourLevels(CommentSoap commentSoap);
    }
}
