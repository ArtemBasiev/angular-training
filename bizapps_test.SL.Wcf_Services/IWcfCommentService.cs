using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.SL.Wcf_Services.DataContracts;

namespace bizapps_test.SL.Wcf_Services
{
    [ServiceContract]
    public interface IWcfCommentService
    {
        [OperationContract]
        int CreateComment(CommentDC commentDC, int postId);

        [OperationContract]
        int UpdateComment(CommentDC commentDC);

        [OperationContract]
        int DeleteComment(CommentDC commentDC);

        [OperationContract]
        IEnumerable<CommentDC> GetIndependentComments(int postId);

        [OperationContract]
        IEnumerable<CommentDC> GetCommentAnswers(int commentId);

        [OperationContract]
        CommentDC GetComment(int commentId);

        [OperationContract]
        bool HaveMoreThanFourLevels(CommentDC commentDC);
    }
}
