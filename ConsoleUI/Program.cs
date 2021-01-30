using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //IoC container olmadığından DataAcessLayer'ıda referans projelr arasına ekledik.

            ProductManager productManager = new ProductManager(new InMemoryProductDal());

            Console.WriteLine("Ürün - Fiyat");
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }


        }
    }
}
