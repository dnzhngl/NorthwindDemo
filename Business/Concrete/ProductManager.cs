using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)   //productmanager bir yerde newlendiği zaman bana bir tane productDal ver.
        {
            _productDal = productDal;
        }

        // AOP : Hata yöneitmi, loglama yönetimi, validasyon işlemleri
        // [LogAspect] : logla
        // [Validate] : Validasyon yap
        // [RemoveCache] : cache'i temizle
        public IResult Add(Product product)
        {
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.Products.Exists(product.ProductName));
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.Products.Add(product.ProductName));
        }
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true, "Ürün sistemden silindi");
        }
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result(true, "Ürün başarıyla güncellendi.");
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 23)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails());
        }

        public IDataResult<ProductDetailDto> GetProductDetail(int productId)
        {
            return new SuccessDataResult<ProductDetailDto>(_productDal.GetProductDetail(productId));
        }
    }
}
