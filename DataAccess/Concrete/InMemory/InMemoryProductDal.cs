using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new Product{ ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock = 65},
                new Product{ ProductId=2, CategoryId=1, ProductName="Tabak", UnitPrice=20, UnitsInStock = 25 },
                new Product{ ProductId=3, CategoryId=2, ProductName="Camera", UnitPrice=500, UnitsInStock = 3},
                new Product{ ProductId=4, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock = 8},
                new Product{ ProductId=5, CategoryId=2, ProductName="Laptop", UnitPrice=3500, UnitsInStock = 2}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            #region Linq olmasaydı
            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (p.ProductId == product.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}
            //_products.Remove(productToDelete);
            #endregion
            var productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(int productId)
        {
            var product = _products.SingleOrDefault(p => p.ProductId == productId);
            return product;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            var query = filter.Compile();
            return (Product)_products.SingleOrDefault(query.Invoke);
        }

        //public List<Product> GetAll()
        //{
        //    return _products;
        //}

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            if (filter == null)
            {
                return _products;
            }
            else
            {
                var query = filter.Compile();
                return _products.Where(query.Invoke).ToList();
            }
        }

        //public List<Product> GetAllByCategory(int categoryId)
        //{
        //    var productList = _products.Where(p => p.CategoryId == categoryId).ToList();
        //    return productList;
        //}

        public void Update(Product product)
        {
            // Gönderdiğin ürün id'sine sahip olan listedeki ürünü bul. 
            // Onun referansını productToUpdate dediğim değişkene ata.
            var productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
