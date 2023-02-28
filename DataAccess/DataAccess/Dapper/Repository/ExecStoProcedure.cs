using Dapper;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Repository
{
    public class ExecStoProcedure
    {
        private static IDbConnection Connection
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EmergencyDepartmentContext"].ConnectionString;
                return new SqlConnection(connectionString);
            }
        }
        public static void NoResult(string nameSP, object objParam)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Query(nameSP, objParam, null, true, 240, commandType: CommandType.StoredProcedure);
            }
        }
        public static void NoResultForLog(string nameSP, LogTmp objParam)
        {

            using (IDbConnection cn = Connection)
            {
                var p = new DynamicParameters();
                p.Add("@CreatedBy", objParam.CreatedBy);
                p.Add("@Ip", objParam.Ip);
                p.Add("@URI", objParam.URI);
                p.Add("@Action", objParam.Action);
                p.Add("@Request", objParam.Request);
                p.Add("@Response", objParam.Response);
                cn.Query(nameSP, p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
