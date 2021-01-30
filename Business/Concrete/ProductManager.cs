using Business.Abstract;
using DataAccess.Abstract;
using Entiities.Concrete;
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

        public Product Get(int productId)
        {
            return _productDal.Get(productId);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }
    }
}
