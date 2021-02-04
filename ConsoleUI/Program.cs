using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //IoC container olmadığından DataAcessLayer'ıda referans projeler arasına ekledik.

            ProductManager productManager = new ProductManager(new EfProductDal());

            Console.WriteLine("Ürün - Fiyat");
            foreach (var product in productManager.GetAllByUnitPrice(40,100))
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }


        }
    }
}
