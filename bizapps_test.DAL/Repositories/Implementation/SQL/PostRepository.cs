using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class PostRepository : IPostRepository
    {
        private readonly ICommentBuilder _commentBuilder;

        private readonly ICommentRepository _commentRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IPostBuilder _postBuilder;

        public IUnitOfWork UnitOfWork { get; }

        internal PostRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _commentBuilder = new CommentBuilder();
            _postBuilder = new PostBuilder();
            _commentRepository = new CommentRepository(uow);
            _categoryRepository = new CategoryRepository(uow);

        }

        public Post GetEntityById(int postId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = string.Format("select * from GetPostById({0})", postId);

                    Post receivedPost = null;
                    using (var reader = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            receivedPost = GetPostViaSqlReader(reader);
                        }
                        
                        reader.Close();
                    }
                    AddCategoriesToPost(receivedPost);
                    AddCommentsToPost(receivedPost);
                    return receivedPost;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public IEnumerable<Post> GetPostsByBlogId(int blogId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = string.Format("select * from GetPostsByBlogId({0})", blogId);

                    IEnumerable<Post> postList;
                    using (var reader = cmd.ExecuteReader())
                    {
                        postList = GetPostListViaSqlReader(reader);
                    }

                    return postList;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public void AddCategoryToPost(Post post, Category categoryToAdd)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "AddCategoryToPost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PostId", SqlDbType.Int){Value = post.Id });
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int){Value = categoryToAdd.Id });

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public void RemoveCategoryFromPost(Post post, Category categoryToRemove)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeleteCategoryFromPost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PostId", SqlDbType.Int){Value = post.Id });
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int){Value = categoryToRemove.Id });

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public int CreateEntity(Post postToCreate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "CreatePost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BlogId", SqlDbType.Int){Value = postToCreate.RelatedTo.Id });
                    cmd.Parameters.Add(new SqlParameter("@PostTitle", SqlDbType.VarChar, 50){Value = postToCreate.PostTitle });
                    cmd.Parameters.Add(new SqlParameter("@PostContent", SqlDbType.Text){Value = postToCreate.PostContent });
                    cmd.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = postToCreate.CreationDate });
                    var outPutParameter = new SqlParameter("@CreatedPostId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@CreatedPostId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();

                    int createdPostId = Convert.ToInt32(outPutParameter.Value);
                    _postBuilder.SetPostId(postToCreate, createdPostId);

                    return createdPostId;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public int UpdateEntity(Post postToUpdate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "UpdatePost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PostId", SqlDbType.Int){Value = postToUpdate.Id });
                    cmd.Parameters.Add(new SqlParameter("@PostTitle", SqlDbType.VarChar, 50){Value = postToUpdate.PostTitle});
                    cmd.Parameters.Add(new SqlParameter("@PostContent", SqlDbType.Text){Value = postToUpdate.PostContent });
                    var outPutParameter = new SqlParameter("@UpdatedPostId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@UpdatedPostId", DataRowVersion.Original, null);
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

        public int DeleteEntity(Post postToDelete)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeletePost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PostId", SqlDbType.Int){Value = postToDelete.Id });
                    var outPutParameter = new SqlParameter("@DeletedPostId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@DeletedPostId", DataRowVersion.Original, null);
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


        public IEnumerable<Post> GetPostsByCategoryId(int categoryId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = string.Format("select * from GetPostsByCategoryId({0})", categoryId);

                    IEnumerable<Post> postList;
                    using (var reader = cmd.ExecuteReader())
                    {
                        postList = GetPostListViaSqlReader(reader);
                    }

                    return postList;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        private IEnumerable<Post> GetPostListViaSqlReader(IDataReader readerSource)
        {
            var receivedPostList = new List<Post>();
            while (readerSource.Read())
            {
                var receivedPost = GetPostViaSqlReader(readerSource);
                receivedPostList.Add(receivedPost);
            }
            readerSource.Close();
            foreach (var post in receivedPostList)
            {
                AddCategoriesToPost(post);
                AddCommentsToPost(post);
            }
            return receivedPostList;
        }

        private Post GetPostViaSqlReader(IDataReader readerSource)
        {
            var receivedPost = _postBuilder.CreatePost(Convert.ToString(readerSource["PostTitle"]),
                Convert.ToString(readerSource["PostContent"]), Convert.ToDateTime(readerSource["CreationDate"]));
            var receivedpostId = Convert.ToInt32(readerSource["PostId"]);
            _postBuilder.SetPostId(receivedPost, receivedpostId);
            return receivedPost;
        }

        private void AddCategoriesToPost(Post post)
        {
            var postCategories = _categoryRepository.GetPostCategories(post.Id);

            foreach (var category in postCategories)
            {
                post.AddCategory(category);
            }           
        }

        private void AddCommentsToPost(Post post)
        {
            var postComments = _commentRepository.GetPostComments(post.Id);

            foreach (var comment in postComments)
            {
                post.AddComment(comment);
            }
        }
    }
}