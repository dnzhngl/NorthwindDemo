using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        /// <summary>
        /// Brings detailed information of the product corresponding to the entered Id parameter.
        /// </summary>
        /// <param name="productId">Takes the product id of integer type as a parameter.</param>
        /// <returns>Returns product detail in a type of ProductDetailDto</returns>
        public ProductDetailDto GetProductDetail(int productId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             where p.ProductId == productId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets all of the products details in a type of ProductDetailDto.
        /// </summary>
        /// <returns>Returns list of products in a type of ProductDetialDto</returns>
        public List<ProductDetailDto> GetProductsDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // result IQueryable döner. 
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
