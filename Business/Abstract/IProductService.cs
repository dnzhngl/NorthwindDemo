using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);

        IDataResult<List<ProductDetailDto>> GetProductDetails();  // Dto döndürür
        IDataResult<ProductDetailDto> GetProductDetail(int productId);  
    }
}
