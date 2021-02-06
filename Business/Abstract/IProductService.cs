using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product Get(int productId);
        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);
        List<Product> GetAllByCategoryId(int categoryId);
        List<Product> GetAllByUnitPrice(decimal min, decimal max);

        List<ProductDetailDto> GetProductDetails();  // Dto döndürür
    }
}
