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

            #region InMemoryProductDal
            //ProductManager productManager2 = new ProductManager(new InMemoryProductDal());
            //foreach (var product in productManager2.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            //}
            //var product2 = productManager2.Get(1);
            //Console.WriteLine($"{product2.ProductName} {product2.UnitPrice}");
            #endregion

            ProductManager productManager = new ProductManager(new EfProductDal());

            Console.WriteLine("Ürün - Fiyat");
            foreach (var product in productManager.GetAllByUnitPrice(40, 100))
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
        }
    }
}
