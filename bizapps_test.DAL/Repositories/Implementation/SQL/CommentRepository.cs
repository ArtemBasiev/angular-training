using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IBlogUserRepository _blogUserRepository;

        private readonly ICommentBuilder _commentBuilder;

        public IUnitOfWork UnitOfWork { get; }

        internal CommentRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _commentBuilder = new CommentBuilder();
            _blogUserRepository = new BlogUserRepository(uow);

        }

        public Comment GetEntityById(int commentId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetCommentById({0})", commentId);

                try
                {
                    Comment receivedComment = null;
                    using (var reader = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            receivedComment = GetCommentViaSqlReader(reader);
                            reader.Close();
                            var commentCreator = _blogUserRepository.GetUserByCommentId(receivedComment.Id);
                            _commentBuilder.SetUserCreatedBy(receivedComment, commentCreator);
                        }
                    }

                    return receivedComment;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public IEnumerable<Comment> GetPostComments(int postId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetPostComments({0}) order by CreationDate desc", postId);
                return GetCommentListBySqlCommand(cmd);
            }
        }

        public IEnumerable<Comment> GetUserComments(int userId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetUserComments({0}) order by CreationDate desc", userId);
                return GetCommentListBySqlCommand(cmd);
            }
        }


        public int CreateEntity(Comment commentToCreate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "CreateComment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CommentText", SqlDbType.VarChar, 500){Value = commentToCreate.CommentText });
                    cmd.Parameters.Add(new SqlParameter("@PostId", SqlDbType.Int){Value = commentToCreate.RelatedTo.Id });
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int){Value = commentToCreate.CreatedBy.Id });
                    cmd.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime){Value = commentToCreate.CreationDate });
                    var outPutParameter = new SqlParameter("@CreatedCommentId", SqlDbType.Int, 50,         
                        ParameterDirection.InputOutput, false, 0, 0, "@CreatedCommentId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);  
                    
                    cmd.ExecuteNonQuery();

                    int createdCommentId = Convert.ToInt32(outPutParameter.Value);
                    _commentBuilder.SetCommentId(commentToCreate, createdCommentId);

                    return createdCommentId;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public int UpdateEntity(Comment commentToUpdate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "UpdateComment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CommentId", SqlDbType.Int){Value = commentToUpdate.Id });
                    cmd.Parameters.Add(new SqlParameter("@CommentText", SqlDbType.VarChar, 500){Value = commentToUpdate.CommentText });
                    var outPutParameter = new SqlParameter("@UpdatedCommentId", SqlDbType.Int, 50,          
                        ParameterDirection.InputOutput, false, 0, 0, "@UpdatedCommentId", DataRowVersion.Original, null);
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

        public int DeleteEntity(Comment commentToDelete)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeleteComment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CommentId", SqlDbType.Int){Value = commentToDelete.Id });
                    var outPutParameter = new SqlParameter("@DeletedCommentId", SqlDbType.Int, 50, ParameterDirection.InputOutput, false, 0, 0, "@DeletedCommentId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);
               
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(outPutParameter.Value);
                }
                catch (SqlException e)
                {
                    throw new ApplicationException(e.Message);
                }
            }
        }


        private IEnumerable<Comment> GetCommentListBySqlCommand(IDbCommand cmd)
        {
            try
            {
                IEnumerable<Comment> recivedComments;
                using (var reader = cmd.ExecuteReader())
                {
                    recivedComments = GetCommentListViaSqlReader(reader);
                }

                return recivedComments;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }


        private IEnumerable<Comment> GetCommentListViaSqlReader(IDataReader readerSource)
        {
            var commentList = new List<Comment>();
            while (readerSource.Read())
            {
                commentList.Add(GetCommentViaSqlReader(readerSource));
            }
            readerSource.Close();

            foreach (var comment in commentList)
            {
                var commentCreator = _blogUserRepository.GetUserByCommentId(comment.Id);
                _commentBuilder.SetUserCreatedBy(comment, commentCreator);
            }

            return commentList;
        }


        private Comment GetCommentViaSqlReader(IDataReader readerSource)
        {
            var receivedComment = _commentBuilder.CreateComment(Convert.ToString(readerSource["CommentText"]),
                Convert.ToDateTime(readerSource["CreationDate"]));
            var receivedCommentId = Convert.ToInt32(readerSource["CommentId"]);

            _commentBuilder.SetCommentId(receivedComment, receivedCommentId);

            return receivedComment;
        }
    }
}