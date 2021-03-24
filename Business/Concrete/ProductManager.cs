using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        // İş kurallarını operasyonlar içerisine yazmak dry prenibine aykırı olabilir. Eğer iş kuralı kendini tekrar edecek bir kural ise, o kuralı method halinde yaz.

        // Bir EntityManager, kendisi hariç başka bir dalı enjekte edemez!
        // Başka bir entityi ilgilendiren bir durum söz konusu olduğunda o entitiynin SERViCE'ı injekte edilir.
        private readonly IProductDal _productDal;
        private ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)   //productmanager bir yerde newlendiği zaman bana bir tane productDal ver.
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        // AOP : Hata yönetimi, loglama yönetimi, validasyon işlemleri
        #region AOP:
        // İş katmanındaki operasyonlar içerisinde teker teker benzer kontrolleri yapmaktansa bu kontrolleri attribute'lara çeviriip gerekli olan operasyonlara ekliyoruz.  Burada AOP uygulamasaydık bir operasyonun içerisinde yer alacak olan standart kodları simgeledik.
        // validation :  gönderilen nesnenin işi kurallarına dhail olması için yapısal olarak uyumlu olup olmadığını kontrol etmek
        // ValidationTool.Validate(new ProductValidator(), product);
        // Loglama
        // CacheRemove
        // Performance
        // Transaction
        // Authorization

        // business codes
        #endregion
        // [LogAspect] : logla
        // [Validate] : Validasyon yap
        // [RemoveCache] : cache'i temizle
        // Korunan operasyon/method - Claimler (product.add, admin vb. kllanıcının claimleri.
        [SecuredOperation("product.add,admin")] //--> - bu metoda kimler ulaşabilir,operaston bazında yetkilendirme, 
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] //--> ürün güncellendiği zaman cachedeki tüm getleri siler.
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                               CheckIfProductNameIsExists(product.ProductName),
                                               CheckIfCategoryLimitExceeded());
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.Products.Add(product.ProductName));
        }

        // [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true, "Ürün sistemden silindi");
        }

        [ValidationAspect(typeof(ProductValidator))]
        // [CacheRemoveAspect("Get")] -> bellekteki tüm getleri sil anlamına gelir.
        [CacheRemoveAspect("IProductService.Get")] //--> ürün güncellendiği zaman cachedeki tüm getleri siler.
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                if (CheckIfProductNameIsExists(product.ProductName).Success)
                {
                    _productDal.Update(product);
                    return new Result(true, "Ürün başarıyla güncellendi.");
                }
                return new ErrorResult(Messages.Products.Exists(product.ProductName));
            }
            return new ErrorResult();
        }

        [CacheAspect]
        [PerformanceAspect(5)] // metodun çalışması 5 sanineyi geçerse beni uyar.
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        // Cache: bir kulanıcı bir kere kategorileri listelediğinde onu cache atıp, başkası getall metodunu kullandığında database gitmek yerine cachedeb veri döndürülür.
        // Cachelemek istediğimiz datayı key value pair ile bellekte turuyoruz. Key : Cache verdiğimiz isimi temsil eder. 
        //örn: ProductManager.GetAll diyebilir. Parametreli olan metodların cacheini ise ProductManager.GetById(id) şeklinde tutulabilir.
        // Cache yapısı için dotnet core içerisindeki InMemory yapısını kullanıyoruz. Ama daha gelişmiş redis gibi yapılarda var.
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
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

        // İş kuralı parçacığı -> private çünkü sadece bu class içerisnde kullanılmasını istiyoruz.
        // iş kurallarını başka dosyalara da taşıyabiliriz.
        //  Bir kategoride en fazla 10 ürün olabilir.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // GetAll(filter) -> veritabanından sadece filtreye uygun olan verileri çekiyor. Yani önce hepsini çekip sonra filtre uyguluyor diye bişey yok.
            int numberOfProducts = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (numberOfProducts >= 10)
            {
                return new ErrorResult(Messages.Products.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        // Aynı isimde ürün eklenemez
        private IResult CheckIfProductNameIsExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.Products.Exists(productName));
            }
            return new SuccessResult();
        }

        // Mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.
        // Product servisini ilgilendiren bir konu olduğu için burada yazdık. CategoryServicine yazmak için onu ilgilendirmesi lazım.
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll().Data.Count();

            if (result > 15)
            {
                return new ErrorResult(Messages.Categories.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }



        // Transactional code
        // sektörde kullanımı
        //public IResult AddTransactionalTest(Product product)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            Add(product);
        //            if (product.UnitPrice < 10)
        //            {
        //                throw new Exception("");
        //            }
        //            Add(product);

        //            scope.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            scope.Dispose();
        //        }
        //    }

        //    return null;
        //}
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
}
