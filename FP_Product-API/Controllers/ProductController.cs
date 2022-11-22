using System.Collections.Generic;
using System.Linq;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using FP_Product_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FP_Product_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService; 
        }

        [HttpGet("ProductData/GetDefaultProductData")]
        public IEnumerable<ProductData> GetDefaultProductData()
        {
            return _productService.GetDefaultProductData();
        }

        [HttpGet("ProductData/GetById")]
        public ProductData? GetById(int Id, [FromBody]IEnumerable<ProductData> data)
        {
            return _productService.Get(Id, data);
        }

        [HttpGet("ProductData/ProductWithMostBottles")]
        public ProductData ProductWithMostBottles([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.MostBottles(data);
        }

        [HttpGet("ProductData/GetMostExpensiveCheapestProduct")]
        public ProductData GetMostExpensiveProduct([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.MostExpensiveCheapestProduct(data);
        }

        [HttpGet("ProductData/SearchProductsByPrice")]
        public IEnumerable<ProductData> SearchProductsByPrice(double price, [FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByPrice(data, price);
        }

        [HttpGet("ProductData/SearchProductsByDefaultPrice")]
        public IEnumerable<ProductData> SearchProductsByDefaultPrice([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByDefaultPrice(data);
        }
    }
}