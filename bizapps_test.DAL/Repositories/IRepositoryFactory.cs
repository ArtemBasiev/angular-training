using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizapps_test.DAL.Repositories
{
    public interface IRepositoryFactory
    {
        IPostRepository CreatePostRepository(IUnitOfWork uow);

        ICategoryRepository CreateCategoryRepository(IUnitOfWork uow);

        IBlogRepository CreateBlogRepository(IUnitOfWork uow);

        IBlogUserRepository CreateUserRepository(IUnitOfWork uow);

        ICommentRepository CreateCommentRepository(IUnitOfWork uow);
    }
}
