using System.Collections.Generic;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Interfaces
{
    public interface IPostService
    {
        int CreatePost(PostViewModel postDto, int userId, List<CategoryViewModel> categoryListDto);

        int UpdatePost(PostViewModel postDto, List<CategoryViewModel> categoryListDto);

        int DeletePost(PostViewModel postDto);

        IEnumerable<PostViewModel> GetUserPosts(int userId);

        IEnumerable<PostViewModel> GetUserPostsByUserName(string userName);

        IEnumerable<PostViewModel> GetPostsByUserNameWithoutCategory(string userName);

        IEnumerable<PostViewModel> GetPostsByUserNameAndCategory(string userName, int categoryId);

        PostViewModel GetPost(int postId);
    }
}
