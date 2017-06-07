using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using MicroORMDojo.Dapper.Models;

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
                playground.DoFunnyThings(conn);
            }

            Console.ReadKey();
        }

        public void DoFunnyThings(IDbConnection conn)
        {
            // here comes the...
        }
    }
}
