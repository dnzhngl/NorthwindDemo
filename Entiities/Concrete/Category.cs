using Entiities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entiities.Concrete
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
