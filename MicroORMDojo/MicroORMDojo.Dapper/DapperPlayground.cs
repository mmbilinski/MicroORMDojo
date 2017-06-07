using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MicroORMDojo.Dapper
{
    class DapperPlayground
    {
        private static IDbConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString);
        }

        static void Main(string[] args)
        {
            var playground = new DapperPlayground();

            using (var conn = GetSqlConnection())
            {
                // here comes the...
            }

            Console.ReadKey();
        }
    }
}
