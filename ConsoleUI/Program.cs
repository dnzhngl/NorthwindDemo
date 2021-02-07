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

            // ProductTest();
            // CategoryTest();
            // OrderTest();

        }

        private static void OrderTest()
        {
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            var order = orderManager.GetOrderDetail(10248);
            Console.WriteLine($"{order.OrderId} - {order.CompanyName}  {order.EmployeeName}");
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            Console.WriteLine("Ürün - Kategori");
            //foreach (var product in productManager.GetProductDetails())
            //{
            //    Console.WriteLine($"{product.ProductName} - {product.CategoryName}");
            //}

            var productDetail = productManager.GetProductDetail(1);
            Console.WriteLine($"{productDetail.ProductName} - {productDetail.CategoryName}");
        }
    }
}
