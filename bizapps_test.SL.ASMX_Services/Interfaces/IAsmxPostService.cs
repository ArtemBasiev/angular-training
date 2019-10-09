using bizapps_test.SL.ASMX_Services.SoapEntities;

namespace bizapps_test.SL.ASMX_Services.Interfaces
{
    public interface IAsmxPostService
    {
        int CreatePost(PostSoap postSoap, int userId, CategorySoap[] categoryListSoap);

        int UpdatePost(PostSoap postSoap, CategorySoap[] categoryListSoap);

        int DeletePost(PostSoap postSoap);

        PostSoap[] GetUserPosts(int userId);

        PostSoap[] GetUserPostsByUserName(string userName);

        PostSoap[] GetPostsByUserNameWithoutCategory(string userName);

        PostSoap[] GetPostsByUserNameAndCategory(string userName, int categoryId);

        PostSoap GetPost(int postId);
    }
}
