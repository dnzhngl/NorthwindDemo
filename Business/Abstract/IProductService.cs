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
        /// <summary>
        /// Gets all of the products.
        /// </summary>
        /// <returns>Returns type of IDataResult with data of list of products./returns>
        IDataResult<List<Product>> GetAll();
        /// <summary>
        /// Brings the product correponding to the entered Id parameter.
        /// </summary>
        /// <param name="productId">Takes the product id of integer type as a parameter.</param>
        /// <returns>Returns type of IDataResult with data of the product.</returns>
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        /// <param name="categoryId">Takes the category id of integer type as a parameter.</param>
        /// <returns>Returns type of IDataResult with data of the list of products that are in the given category.</returns>
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
        /// <param name="min">Minimum value of the unit price in a type of decimal</param>
        /// <param name="max">Maximum value of the unit price in a type of decimal</param>
        /// <returns>Returns type of IDataResult with data of the list of products that have unit price between the given min and max values.</returns>
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);
        /// <summary>
        /// Gets all of the products details in a type of ProductDetailDto.
        /// </summary>
        /// <returns>Returns IDataResult with data of list of products in a type of ProductDetialDto</returns>
        IDataResult<List<ProductDetailDto>> GetProductDetails();  // Dto döndürür
        /// <summary>
        /// Brings detailed information of the product corresponding to the entered Id parameter.
        /// </summary>
        /// <param name="productId">Takes the product id of integer type as a parameter.</param>
        /// <returns>Returns IDataResult with data of product detail in a type of ProductDetailDto</returns>
        IDataResult<ProductDetailDto> GetProductDetail(int productId);


        /// <summary>
        /// Transaction testi -> uygulamalarda 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        IResult AddTransactionalTest(Product product);
    }
}
