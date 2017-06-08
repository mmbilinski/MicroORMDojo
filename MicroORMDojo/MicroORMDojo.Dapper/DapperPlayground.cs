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

            // simple select with parameter
            //var query = conn.Query("select * from Categories where CategoryName like @start + '%'", new { start = "C" });

            // select with more parameters
            //var query = conn.Query<Employee>("select FirstName, TitleOfCourtesy from Employees where TitleOfCourtesy in @titles",
            //    new { titles = new[] { "Mrs.", "Ms." } });

            // static-typed join
            //var query = conn.Query<Product, Category, Product>(
            //    "select c.*, p.* from Categories c inner join Products p on p.CategoryID = c.CategoryID",
            //    (p, c) =>
            //    {
            //        p.Category = c;
            //        return p;
            //    }, splitOn: "CategoryID");

            // execute many queries at once
            //using (var query =
            //    conn.QueryMultiple(
            //        "select * from Employees where EmployeeID = @id; select * from EmployeeTerritories where EmployeeID = @id",
            //        new { Id = 1 }))
            //{
            //    var result1 = query.Read();
            //    Console.WriteLine(result1.Single());
            //    var result2 = query.Read();

            //    foreach (var item in result2)
            //    {
            //        Console.WriteLine($"\t{item.TerritoryName}");
            //    }
            //}

            // inserting with scalar parameters
            //var query = conn.QuerySingle<long>("insert into Categories (CategoryName, Description) values (@name, @descr); select scope_identity()",
            //    new { name = "Biscuits", descr = "Sweet cookies" });
            //Console.WriteLine(query);

            // updating/deleting
            //var query = conn.Query("update Categories set CategoryName = @new where CategoryName = @old",
            //    new { old = "Biscuits", @new = "Whatever" });
            //var query = conn.Query("delete from Categories where CategoryName = @cat", new {cat = "Whatever"});

            // stored procedure in two flavours - with anonymous parameters and with dictionary-like DynamicParameter
            // ...and we can see that Dapper nicely handles dates
            //var dp = new DynamicParameters();
            //dp.Add("Beginning_Date", new DateTime(1997, 1, 1));
            //dp.Add("Ending_Date", new DateTime(1997, 2, 28));

            //var query = conn.Query("Sales by Year",
            //    new { Beginning_Date = new DateTime(1997, 1, 1), Ending_Date = new DateTime(1997, 2, 28) }, // flavour #1
            //    dp, // flavour #2
            //    commandType: CommandType.StoredProcedure); // it's important!
            //Console.WriteLine(query.Sum(r => (decimal)r.Subtotal));

            // custom table-mapping (but use [Table] attribute on entities, please)
            //SqlMapperExtensions.TableNameMapper = type =>
            //{
            //    if (type == typeof(Category))
            //        return "Categories";

            //    return $"{type}s";
            //};

            // and here comes static types
            //var query = conn.Insert(new Category() { CategoryName = "New category", Description = "Descr" });
            //Console.WriteLine(query);

            //foreach (var item in query)
            //{
            //    Console.WriteLine($"{item.ProductName} {item.Category.CategoryName}");
            //}

            // Dapper.Contrib package has many nice extensions methods to make Dapper even more friendly, try them all!
            //T Get<T>(id);
            //IEnumerable<T> GetAll<T>();
            //int Insert<T>(T obj);
            //int Insert<T>(Enumerable<T> list);
            //bool Update<T>(T obj);
            //bool Update<T>(Enumerable<T> list);
            //bool Delete<T>(T obj);
            //bool Delete<T>(Enumerable<T> list);
            //bool DeleteAll<T>();
        }
    }
}