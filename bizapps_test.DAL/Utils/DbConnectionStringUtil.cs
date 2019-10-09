using System.Configuration;

namespace bizapps_test.DAL.Utils
{
    public static class DbConnectionStringUtil
    {
        public static string GetConnectionString()
        {
            string connectionString = null;

            var settings = ConfigurationManager.ConnectionStrings["BizappsTestADO"];


            if (settings != null)
                connectionString = settings.ConnectionString;

            return connectionString;
        }
    }
}