using System;
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
    public class ProductDataController : ControllerBase
    {
        private readonly IProductDataService _productService;

        public ProductDataController(
            IProductDataService productService)
        {
            _productService = productService; 
        }

        #region HttpGet

        [HttpGet("GetDefaultProductData")]
        public IEnumerable<ProductData>? GetDefaultProductData(string? url = null)
        {
            return _productService.GetData(url);
        }

        [HttpGet("GetById/{Id}")]
        public ProductData? GetById(int Id, string? url = null)
        {
            return _productService.Get(Id, url);
        }

        [HttpGet("ProductWithMostBottles")]
        public IEnumerable<ProductData>? ProductWithMostBottles(string? url = null)
        {
            return _productService.MostBottles(url);
        }

        [HttpGet("GetMostExpensiveCheapestProduct")]
        public MostExpensiveCheapestProduct GetMostExpensiveProduct(string? url = null)
        {
            return _productService.GetMostExpensiveCheapestProduct(url);
        }

        [HttpGet("SearchProductsByPrice")]
        public IEnumerable<ProductData> SearchProductsByPrice(double price, string? url = null)
        {
            return _productService.SearchProductsByPrice(price, url);
        }

        [HttpGet("SearchProductsByDefaultPrice")]
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(string? url = null)
        {
            return _productService.SearchProductsByDefaultPrice(url);
        }
        #endregion

        #region HttpPost

        [HttpPost("GetById/{Id}")]
        public ProductData? GetById(int Id, [FromBody] IEnumerable<ProductData> data)
        {
            return _productService.Get(Id, data);
        }

        [HttpPost("ProductWithMostBottles")]
        public IEnumerable<ProductData>? ProductWithMostBottles([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.MostBottles(data);
        }

        [HttpPost("GetMostExpensiveCheapestProduct")]
        public MostExpensiveCheapestProduct GetMostExpensiveProduct([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.GetMostExpensiveCheapestProduct(data);
        }

        [HttpPost("SearchProductsByPrice")]
        public IEnumerable<ProductData> SearchProductsByPrice(double price, [FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByPrice(price, data);
        }

        [HttpPost("SearchProductsByDefaultPrice")]
        public IEnumerable<ProductData> SearchProductsByDefaultPrice([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByDefaultPrice(data);
        }
        #endregion
    }
}