using System;
using System.Data;
using System.Data.SqlClient;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class BlogUserRepository : IBlogUserRepository
    {
        private readonly IBlogUserBuilder _blogUserBuilder;

        public IUnitOfWork UnitOfWork { get; }

        internal BlogUserRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _blogUserBuilder = new BlogUserBuilder();
        }


        public BlogUser GetEntityById(int blogUserId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogUserById({0})", blogUserId);
                return GetBlogUserViaSqlCommand(cmd);
            }
        }


        public BlogUser GetBlogUserByName(string userName)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogUserByName('{0}')", userName);

                return GetBlogUserViaSqlCommand(cmd);
            }
        }


        public BlogUser GetBlogUserByBlogId(int blogId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogUserByBlogId({0})", blogId);
                return GetBlogUserViaSqlCommand(cmd);
            }
        }

        public BlogUser GetUserByCommentId(int commentId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetUserByCommentId({0})", commentId);
                return GetBlogUserViaSqlCommand(cmd);
            }
        }

        public int CreateEntity(BlogUser blogUserToCreate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "CreateBlogUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50){Value = blogUserToCreate.UserName });
                    cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.VarChar, 50){Value = blogUserToCreate.UserPassword });
                    var outPutParameter = new SqlParameter("@CreatedUserId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@CreatedUserId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();
                    int createdUserId = Convert.ToInt32(outPutParameter.Value);
                    _blogUserBuilder.SetBlogUserId(blogUserToCreate, createdUserId);

                    return createdUserId;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public int UpdateEntity(BlogUser blogUserToUpdate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "UpdateBlogUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int){Value = blogUserToUpdate.Id });
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50){Value = blogUserToUpdate.UserName });
                    cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.VarChar, 50){Value = blogUserToUpdate.UserPassword });
                    var outPutParameter = new SqlParameter("@UpdatedUserId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@UpdatedUserId", DataRowVersion.Original, null);
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

        public int DeleteEntity(BlogUser blogUser)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeleteBlogUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int){Value = blogUser.Id });
                    var outPutParameter = new SqlParameter("@DeletedUserId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@DeletedUserId", DataRowVersion.Original, null);
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


        private BlogUser GetBlogUserViaSqlCommand(IDbCommand cmd)
        {
            try
            {
                BlogUser receivedBlogUser = null;
                using (var reader = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        receivedBlogUser = GetBlogUserViaSqlReader(reader);
                    }
                    
                    reader.Close();
                }

                return receivedBlogUser;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }


        private BlogUser GetBlogUserViaSqlReader(IDataReader readerSource)
        {
            var receivedBlogUser = _blogUserBuilder.CreateBlogUser(Convert.ToString(readerSource["UserName"]),
                Convert.ToString(readerSource["UserPassword"]));
            _blogUserBuilder.SetBlogUserId(receivedBlogUser, Convert.ToInt32(readerSource["UserId"]));

            return receivedBlogUser;
        }
    }
}