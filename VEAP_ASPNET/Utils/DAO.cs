using System.Data;
using System.Data.SQLite;
using Dapper;
using System;

namespace VEAP_ASPNET.Utils
{
    public class DAO
    {
        private static string connString = @"Data Source=C:\Users\ruiwei\Documents\veap_test.db;Version=3;";
        private static IDbConnection conn = new SQLiteConnection(connString);

        private const string TB_PROJECT = "project";

        public static dynamic GetProjectByUserId(int userId)
        {
            string sql = $"SELECT * FROM {TB_PROJECT} WHERE CreateUserId = '{userId}'";
            Debug.Log(sql);
            return conn.Query(sql);
        }
    }

}