

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class RepositoryFactory: IRepositoryFactory
    {
        public IPostRepository CreatePostRepository(IUnitOfWork uow)
        {
            return new PostRepository(uow);
        }


        public ICategoryRepository CreateCategoryRepository(IUnitOfWork uow)
        {
            return new CategoryRepository(uow);
        }


        public IBlogRepository CreateBlogRepository(IUnitOfWork uow)
        {
            return new BlogRepository(uow);
        }


        public IBlogUserRepository CreateUserRepository(IUnitOfWork uow)
        {
            return new BlogUserRepository(uow);
        }


        public ICommentRepository CreateCommentRepository(IUnitOfWork uow)
        {
            return new CommentRepository(uow);
        }
    }
}
