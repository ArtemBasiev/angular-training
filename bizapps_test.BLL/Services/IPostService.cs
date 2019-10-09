using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.DAL.Repositories;

namespace bizapps_test.BLL.Services
{
    public interface IPostService
    {
        AnswerStatus CreatePost(PostDto postDTO);

        AnswerStatus UpdatePost(PostDto postDTO);

        AnswerStatus DeletePost(PostDto postDTO);

        AnswerStatus DeletePost(PostDto postDTO, IPostRepository postRepository);

        ServiceAnswer<PostDto> GetPostById(int postId);

        ServiceAnswer<IEnumerable<PostDto>> GetBlogPosts(int blogId);
    }
}