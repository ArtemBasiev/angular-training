using System.Collections.Generic;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        IEnumerable<Comment> GetPostComments(int postId);

        IEnumerable<Comment> GetUserComments(int userId);
    }
}