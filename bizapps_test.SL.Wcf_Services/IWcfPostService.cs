using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.SL.Wcf_Services.DataContracts;

namespace bizapps_test.SL.Wcf_Services
{
    [ServiceContract]
    public interface IWcfPostService
    {
        [OperationContract]
        int CreatePost(PostDC postDC, int userId, List<CategoryDC> categoryListDC);

        [OperationContract]
        int UpdatePost(PostDC postDC, List<CategoryDC> categoryListDC);

        [OperationContract]
        int DeletePost(PostDC postDC);

        [OperationContract]
        IEnumerable<PostDC> GetUserPosts(int userId);

        [OperationContract]
        IEnumerable<PostDC> GetUserPostsByUserName(string userName);

        [OperationContract]
        IEnumerable<PostDC> GetPostsByUserNameWithoutCategory(string userName);

        [OperationContract]
        IEnumerable<PostDC> GetPostsByUserNameAndCategory(string userName, int categoryId);

        [OperationContract]
        PostDC GetPost(int postId);
    }
}
