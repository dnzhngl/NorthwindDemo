using Entiities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entiities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; } //smallint
        public decimal UnitPrice { get; set; } 
    }
}
