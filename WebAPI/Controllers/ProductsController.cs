using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Attribute
    public class ProductsController : ControllerBase
    {
        // Loosely coupled - gevşek bağlılık : Bir bağımlılığı var ama soyut'a bağımlı.
        // IoC Container : Inversion of control (değişimin kontrolü) - bellekteki bir liste gibi düşün.
        IProductService _productService;

        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("getall")] //isim verme
        public IActionResult GetAll()
        {
            // Swagger - hazır dökümantasyon ürünüdür :  ben sana bi api veriyorum. bu api böyle kullanılır. şu durumda bu sonucu veerir vs diye açıklar.
            // Dependency chain --
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result); // işlem başarılı
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //------------------------------------------
        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllByCategoryId")]
        public IActionResult GetAllByCategoryId(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllByUnitPrice")]
        public IActionResult GetAllByUnitPrice(decimal min, decimal max)
        {
            var result = _productService.GetAllByUnitPrice(min, max);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetProductDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductDetail")]
        public IActionResult GetProductDetail(int productId)
        {
            var result = _productService.GetProductDetail(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
