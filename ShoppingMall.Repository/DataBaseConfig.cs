using ShoppingMall.Common.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ShoppingMall.Repository
{
    public class DataBaseConfig
    {
        private static string DefaultSqlConnectionString = ConfigHelper.GetSectionValue("ConnectionString");

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }

    public enum ConnectionType
    {
        SqlServer = 0,
        Oracle = 1,
    }
}
