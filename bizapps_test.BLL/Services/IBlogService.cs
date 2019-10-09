using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.DAL.Repositories;

namespace bizapps_test.BLL.Services
{
    public interface IBlogService 
    {
        AnswerStatus CreateBlog(BlogDto blogDTO);

        AnswerStatus CreateBlog(BlogDto blogDTO, IBlogRepository blogRepository);

        AnswerStatus UpdateBlog(BlogDto blogDTO);

        AnswerStatus DeleteBlog(BlogDto blogDTO);

        AnswerStatus DeleteBlog(BlogDto blogDTO, IBlogRepository blogRepository);

        ServiceAnswer<BlogDto> GetBlogById(int blogId);

        ServiceAnswer<BlogDto> GetBlogByUserId(int userId);

        ServiceAnswer<IEnumerable<BlogDto>> GetAllBlogs();
    }
}