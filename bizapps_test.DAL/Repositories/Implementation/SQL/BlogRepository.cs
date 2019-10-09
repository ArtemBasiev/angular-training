using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IBlogBuilder _blogBuilder;

        private readonly IBlogUserBuilder _blogUserBuilder;

        private readonly IBlogUserRepository _blogUserRepository;

        private readonly ICategoryBuilder _categoryBuilder;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IPostBuilder _postBuilder;

        private readonly IPostRepository _postRepository;

        public IUnitOfWork UnitOfWork { get; }

        internal BlogRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _blogBuilder = new BlogBuilder();
            _blogUserBuilder = new BlogUserBuilder();
            _categoryBuilder = new CategoryBuilder();
            _postBuilder = new PostBuilder();
            _postRepository = new PostRepository(uow);
            _categoryRepository = new CategoryRepository(uow);
            _blogUserRepository = new BlogUserRepository(uow);
        }


        public Blog GetEntityById(int blogId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogById({0})", blogId);             

                return GetBlogViaSqlCommand(cmd);
            }
        }


        public Blog GetBlogByUserId(int userId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogByUserId({0})", userId);

                return GetBlogViaSqlCommand(cmd);
            }
        }

        public Blog GetBlogByCategoryId(int categoryId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogByCategoryId({0})", categoryId);

                return GetBlogViaSqlCommand(cmd);
            }
        }


        public Blog GetBlogByPostId(int postId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogByPostId({0})", postId);

                return GetBlogViaSqlCommand(cmd);
            }
        }


        public int CreateEntity(Blog blogToCreate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "CreateBlog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.VarChar, 50){Value = blogToCreate.BlogTitle});
                    cmd.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime){Value = blogToCreate.CreationDate});
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int){Value = blogToCreate.CreatedBy.Id});
                    var outPutParameter = new SqlParameter("@CreatedBlogId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@CreatedBlogId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();

                    int createdBlogId = Convert.ToInt32(outPutParameter.Value);

                    _blogBuilder.SetBlogId(blogToCreate, createdBlogId);

                    return createdBlogId;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public int UpdateEntity(Blog blogToUpdate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "UpdateBlog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BlogId", SqlDbType.Int){Value = blogToUpdate.Id });
                    cmd.Parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.VarChar, 50){Value = blogToUpdate.BlogTitle});
                    var outPutParameter = new SqlParameter("@UpdatedBlogId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@UpdatedBlogId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(outPutParameter.Value);
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public int DeleteEntity(Blog blogToDelete)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeleteBlog";
                    cmd.CommandType = CommandType.StoredProcedure;               
                    cmd.Parameters.Add(new SqlParameter("@BlogId", SqlDbType.Int){Value = blogToDelete.Id });
                    var outPutParameter = new SqlParameter("@DeletedBlogId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@DeletedBlogId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(outPutParameter.Value);
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        private Blog GetBlogViaSqlCommand(IDbCommand cmd)
        {
            try
            {
                Blog receivedBlog = null;
                using (var reader = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if(reader.HasRows)
                    {
                        receivedBlog = GetBlogViaSqlReader(reader);
                    }
                    
                }

                return receivedBlog;
            }
            catch (SqlException e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        private Blog GetBlogViaSqlReader(IDataReader readerSource)
        {
            readerSource.Read();

            var receivedBlog = _blogBuilder.CreateBlog(Convert.ToString(readerSource["BlogTitle"]),
                Convert.ToDateTime(readerSource["CreationDate"]));
            var receivedBlogId = Convert.ToInt32(readerSource["BlogId"]);
            _blogBuilder.SetBlogId(receivedBlog, receivedBlogId);
            readerSource.Close();
            var userRelatedWithBlog = _blogUserRepository.GetBlogUserByBlogId(receivedBlogId);
            _blogUserBuilder.SetUserBlog(userRelatedWithBlog, receivedBlog);
            _blogBuilder.SetBlogUser(receivedBlog, userRelatedWithBlog);

            var receivedBlogCategories = _categoryRepository.GetBlogCategories(receivedBlogId);
            foreach (var categoryToAdd in receivedBlogCategories)
            {
                _categoryBuilder.SetBlogRelatedTo(categoryToAdd, receivedBlog);
                receivedBlog.AddCategory(categoryToAdd);
            }

            var receivedBlogPosts = _postRepository.GetPostsByBlogId(receivedBlogId);
            foreach (var postToAdd in receivedBlogPosts)
            {
                _postBuilder.SetBlogRelatedTo(postToAdd, receivedBlog);
                receivedBlog.AddPost(postToAdd);
            }

            return receivedBlog;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            try
            {
                using (var cmd = UnitOfWork.CreateCommand())
                {
                    cmd.CommandText = "select * from GetAllBlogs()";


                    List<Blog> receivedBlogList = new List<Blog>();
                    using (var reader = (SqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var receivedBlog = _blogBuilder.CreateBlog(Convert.ToString(reader["BlogTitle"]),
                                    Convert.ToDateTime(reader["CreationDate"]));
                                var receivedBlogId = Convert.ToInt32(reader["BlogId"]);
                                _blogBuilder.SetBlogId(receivedBlog, receivedBlogId);
                                reader.Close();
                                var userRelatedWithBlog = _blogUserRepository.GetBlogUserByBlogId(receivedBlogId);
                                _blogUserBuilder.SetUserBlog(userRelatedWithBlog, receivedBlog);
                                _blogBuilder.SetBlogUser(receivedBlog, userRelatedWithBlog);

                                receivedBlogList.Add(receivedBlog); 
                            } 
                        }

                    }

                    return receivedBlogList;
                }
            }
            catch (SqlException e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}