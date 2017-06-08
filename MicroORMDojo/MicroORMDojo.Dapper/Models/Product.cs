using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace MicroORMDojo.Dapper.Models
{
    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
