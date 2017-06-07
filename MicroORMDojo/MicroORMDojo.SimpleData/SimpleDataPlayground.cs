using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace MicroORMDojo.SimpleData
{
    class SimpleDataPlayground
    {
        private static dynamic GetSqlConnection()
        {
            return Database.OpenNamedConnection("Northwind");
        }

        static void Main(string[] args)
        {
            var playground = new SimpleDataPlayground();

            using (var db = GetSqlConnection())
            {
                playground.DoFunnyThings(db);
            }

            Console.ReadKey();
        }

        public void DoFunnyThings(dynamic db)
        {
            // here comes the...
        }
    }
}
