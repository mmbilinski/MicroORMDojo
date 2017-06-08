using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroORMDojo.SimpleData.Models;
using Simple.Data;

namespace MicroORMDojo.SimpleData
{
    class SimpleDataPlayground
    {
        private const string Line = "====================================";

        private static dynamic GetSqlConnection()
        {
            return Database.OpenNamedConnection("Northwind");
        }

        static void Main(string[] args)
        {
            var playground = new SimpleDataPlayground();

            var db = GetSqlConnection();
            playground.DoFunnyThings(db);

            Console.ReadKey();
        }

        public void DoFunnyThings(dynamic db)
        {
            var employees = db.Employees.All();

            foreach (var e in employees)
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            PrintLineAndWaitForKey();

            PrintSteves(db);
            PrintLineAndWaitForKey();

            var robert = db.Employees.FindByFirstName("Robert");
            if (robert != null)
            {
                robert.FirstName = "Steven";
                db.Employees.Update(robert);
            }

            PrintSteves(db);
            PrintLineAndWaitForKey();

            dynamic newSteven = new ExpandoObject();
            newSteven.FirstName = "Steven";
            newSteven.LastName = "Kowalski";

            var newStevenInsert = db.Employees.Insert(newSteven);
            Console.WriteLine($"New Steve's ID is {newStevenInsert.EmployeeID}");

            PrintSteves(db);
            PrintLineAndWaitForKey();

            var anotherSteven = db.Employees.Insert(FirstName: "Steven", LastName: "Maliniak");
            PrintSteves(db);
            PrintLineAndWaitForKey();

            var aaaaandHereComesTheStaticSteven =
                db.Employees.Insert(new Employee() {FirstName = "Steven", LastName = "Statyczny"});
            PrintSteves(db);
            PrintLineAndWaitForKey();

            var isSteveKowalskiStillHere = db.Employees.FindByFirstNameAndLastName("Steven", "Kowalski");
            Console.WriteLine(isSteveKowalskiStillHere != null ? "Steve Kowalski is here" : "There are no more Steves Kowalski");
            PrintLineAndWaitForKey();

            var removingStevens = db.Employees.FindAllByFirstName("Steven");
            foreach (var poorSteven in removingStevens)
            {
                try
                {
                    db.Employees.DeleteByEmployeeID(poorSteven.EmployeeID);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Unable to delete {poorSteven.FirstName} {poorSteven.LastName}");
                }
                //db.Employees.DeleteAll(db.Employees.FirstName.Equals("Steven"));
            }

            PrintSteves(db);
            PrintLineAndWaitForKey();

            //var employeesAndTerritories = db.Employees.All().WithEmployeeTerritories().WithTerritories();
            var employeesAndTerritories = db.Employees.All()
                .LeftJoin(db.EmployeeTerritories)
                .On(db.Employees.EmployeeID == db.EmployeeTerritories.EmployeeID)
                .LeftJoin(db.Territories)
                .On(db.Territories.TerritoryID == db.EmployeeTerritories.TerritoryID)
                .Select(db.Employees.FirstName, db.Employees.LastName, db.Territories.TerritoryDescription);

            foreach (var et in employeesAndTerritories)
                Console.WriteLine($"{et.FirstName} {et.LastName} @ {et.TerritoryDescription}");

            Console.WriteLine("end");
        }

        private void PrintLineAndWaitForKey()
        {
            Console.WriteLine(Line);
            Console.ReadKey(true);
        }

        private static void PrintSteves(dynamic db)
        {
            var steves = db.Employees.FindAllByFirstName("Steven");
            foreach (var steve in steves)
                Console.WriteLine($"{steve.FirstName} {steve.LastName}");
        }
    }
}
