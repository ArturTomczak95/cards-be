using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Sql
{
    public static class SqlDataAccess
    {
        public static List<T> LoadData<T>(string sql, string connectionString)
        {
            IDbConnection cnn = new SqlConnection(connectionString);
            return cnn.Query<T>(sql).ToList();
        }

        public static int SaveData<T>(string sql, T data, string connectionString)
        {
            IDbConnection cnn = new SqlConnection(connectionString);
            return cnn.Execute(sql, data);
        }

        public static int RemoveData(string sql, string connectionString)
        {
            IDbConnection cnn = new SqlConnection(connectionString);
            return cnn.Execute(sql);
        }
    }
}
