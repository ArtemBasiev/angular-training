using System.Data.SqlClient;
using bizapps_test.DAL.Utils;

namespace bizapps_test.DAL.Repositories.Implementation.SQL
{
    public class AdoNetUnitOfWorkFactory: IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            var connection = new SqlConnection(DbConnectionStringUtil.GetConnectionString());
            connection.Open();

            return new AdoNetUnitOfWork(connection);
        }
    }
}
