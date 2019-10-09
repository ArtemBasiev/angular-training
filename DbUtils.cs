using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



   public class DBUtils
    {
       public static SqlConnection GetDBConnection()
       {
           string datasource = @"DESKTOP-5QPRQC5\SQLEXPRESS";
           string database = "BizappsTest";
           return DBSQLServerUtils.GetDBConnection(datasource, database);

       }
    }



