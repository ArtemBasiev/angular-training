using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bizapps_test.Domain.ModelBuilders.Abstract;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryBuilder _categoryBuilder;

        public IUnitOfWork UnitOfWork { get; }

        internal CategoryRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _categoryBuilder = new CategoryBuilder();

        }


        public Category GetEntityById(int categoryId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = string.Format("select * from GetCategoryById({0})", categoryId);
                    Category receivedCategory = null;
                    using (var reader = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            receivedCategory = GetCategoryViaSqlReader(reader);
                        }
                        reader.Close();
                    }

                    return receivedCategory;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }


        public IEnumerable<Category> GetBlogCategories(int blogId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetBlogCategories({0})", blogId);
                return GetCategoryListViaSqlCommand(cmd);
            }
        }

        public IEnumerable<Category> GetPostCategories(int postId)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from GetPostCategories({0})", postId);
                return GetCategoryListViaSqlCommand(cmd);
            }
        }

        public int CreateEntity(Category categoryToCreate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "CreateCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.VarChar, 50){Value = categoryToCreate.CategoryName });
                    cmd.Parameters.Add(new SqlParameter("@BlogId", SqlDbType.Int){Value = categoryToCreate.RelatedTo.Id });
                    var outPutParameter = new SqlParameter("@CreatedCategoryId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@CreatedCategoryId", DataRowVersion.Original, null);
                    outPutParameter.Value = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(outPutParameter);

                    cmd.ExecuteNonQuery();

                    int createdCategoryId = Convert.ToInt32(outPutParameter.Value);

                    _categoryBuilder.SetCategoryId(categoryToCreate, createdCategoryId);

                    return createdCategoryId;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        public int UpdateEntity(Category categoryToUpdate)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "UpdateCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int){Value = categoryToUpdate.Id });
                    cmd.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.VarChar, 50){Value = categoryToUpdate.CategoryName });
                    var outPutParameter = new SqlParameter("@UpdatedCategoryId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@UpdatedCategoryId", DataRowVersion.Original, null);
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

        public int DeleteEntity(Category category)
        {
            using (var cmd = UnitOfWork.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "DeleteCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int){Value = category.Id });
                    var outPutParameter = new SqlParameter("@DeletedCategoryId", SqlDbType.Int, 50,
                        ParameterDirection.InputOutput, false, 0, 0, "@DeletedCategoryId", DataRowVersion.Original, null);
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


        private IEnumerable<Category> GetCategoryListViaSqlCommand(IDbCommand cmd)
        {
            try
            {

                IEnumerable<Category> receivedCategories;
                using (var reader = cmd.ExecuteReader())
                {
                    receivedCategories = GetCategoryListViaSqlReader(reader);
                }

                return receivedCategories;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        private List<Category> GetCategoryListViaSqlReader(IDataReader readerSource)
        {
            var categoryList = new List<Category>();
            while (readerSource.Read())
            {
                categoryList.Add(GetCategoryViaSqlReader(readerSource));
            }
            readerSource.Close();

            return categoryList;
        }

        private Category GetCategoryViaSqlReader(IDataReader readerSource)
        {
            var receivedCategory = _categoryBuilder.CreateCategory(Convert.ToString(readerSource["CategoryName"]));
            var receivedCategoryId = Convert.ToInt32(readerSource["CategoryId"]);
            _categoryBuilder.SetCategoryId(receivedCategory, receivedCategoryId);

            return receivedCategory;
        }
    }
}