using Dapper;
using DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper
{
    public static class DapperExtensions
    {
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }
        public static void Inserts<T>(this IDbConnection cnn, string tableName, List<T> param)
        {
            string query = DynamicQuery.GetInsertQuery(tableName, param[0]);
            cnn.Execute(query, param, commandTimeout: 240);
        }

        public static void Update(this IDbConnection cnn, string tableName, dynamic param)
        {
           SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param, commandTimeout: 240);
        }
        public static void Updates<T>(this IDbConnection cnn, string tableName, List<T> param)
        {
            string query = DynamicQuery.GetUpdateQuery(tableName, param[0]);
            cnn.Execute(query,param, commandTimeout: 240);
        }
    }
}
