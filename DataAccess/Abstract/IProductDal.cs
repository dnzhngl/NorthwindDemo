using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        /// <summary>
        /// Gets all of the products details in a type of ProductDetailDto.
        /// </summary>
        /// <returns>Returns list of products in a type of ProductDetialDto</returns>
        List<ProductDetailDto> GetProductsDetails();
        /// <summary>
        /// Brings detailed information of the product corresponding to the entered Id parameter.
        /// </summary>
        /// <param name="productId">Takes the product id of integer type as a parameter.</param>
        /// <returns>Returns product detail in a type of ProductDetailDto</returns>
        ProductDetailDto GetProductDetail(int productId);
    }
}

// Code Refactoring - Kodun iyileştirilmesi