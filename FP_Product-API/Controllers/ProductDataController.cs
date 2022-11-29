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

        /// <summary>
        /// Gets a list of ProductData by the default url("https://flapotest.blob.core.windows.net/test/ProductData.json") or a custom url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("GetProductData")]
        public IEnumerable<ProductData>? GetProductData(string? url = null)
        {
            return _productService.GetObjectData(url);
        }

        /// <summary>
        /// Trys to get an entry from the default ProductData-List or an given url
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("GetById/{Id}")]
        public ProductData? GetById(int Id, string? url = null)
        {
            return _productService.Get(Id, url);
        }

        /// <summary>
        /// Gets the products with the most bottles, shows multiple entrys if they have the same amount.
        /// Data is used from the default ProductData-List or an given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("ProductWithMostBottles")]
        public IEnumerable<ProductData>? ProductWithMostBottles(string? url = null)
        {
            return _productService.MostBottles(url);
        }

        /// <summary>
        /// Gets the most expensive/cheapest products , shows multiple entrys if they have the same price.
        /// Data is used from the default ProductData-List or an given url
        /// </summary>
        /// <param name="url" required="true"></param>
        /// <returns></returns>
        [HttpGet("GetMostExpensiveCheapestProduct")]
        public MostExpensiveCheapestProduct GetMostExpensiveProduct(string? url = null)
        {
            return _productService.GetMostExpensiveCheapestProduct(url);
        }

        /// <summary>
        /// Gets the products with a given price, shows multiple entrys if they have the same amount (orderd by price per litre).
        /// Data is used from the default ProductData-List or an given url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet("SearchProductsByPrice/price/{price}")]
        public IEnumerable<ProductData> SearchProductsByPrice(double price, string? url = null)
        {
            return _productService.SearchProductsByPrice(price, url);
        }

        /// <summary>
        /// Gets the products with the price 17.99, shows multiple entrys if they have the same amount (orderd by price per litre).
        /// Data is used from the default ProductData-List or an given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("SearchProductsByDefaultPrice")]
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(string? url = null)
        {
            return _productService.SearchProductsByDefaultPrice(url);
        }
        #endregion

        #region HttpPost

        /// <summary>
        /// Trys to get an entry from the default ProductData-List or an given json
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("GetById/{Id}")]
        public ProductData? GetById(int Id, [FromBody] IEnumerable<ProductData> data)
        {
            return _productService.Get(Id, data);
        }

        /// <summary>
        /// Gets the products with the most bottles, shows multiple entrys if they have the same amount.
        /// Data is used from the default ProductData-List or an given json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("ProductWithMostBottles")]
        public IEnumerable<ProductData>? ProductWithMostBottles([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.MostBottles(data);
        }

        /// <summary>
        /// Gets the most expensive/cheapest products, shows multiple entrys if they have the same price.
        /// Data is used from the default ProductData-List or an given json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("GetMostExpensiveCheapestProduct")]
        public MostExpensiveCheapestProduct GetMostExpensiveProduct([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.GetMostExpensiveCheapestProduct(data);
        }

        /// <summary>
        /// Gets the products with a given price, shows multiple entrys if they have the same amount (orderd by price per litre).
        /// Data is used from the default ProductData-List or an given json
        /// </summary>
        /// <param name="data"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost("SearchProductsByPrice")]
        public IEnumerable<ProductData> SearchProductsByPrice(double price, [FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByPrice(price, data);
        }


        /// <summary>
        /// Gets the products with the price 17.99, shows multiple entrys if they have the same amount (orderd by price per litre). 
        /// Data is used from the default ProductData-List or an given json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SearchProductsByDefaultPrice")]
        public IEnumerable<ProductData> SearchProductsByDefaultPrice([FromBody] IEnumerable<ProductData> data)
        {
            return _productService.SearchProductsByDefaultPrice(data);
        }
        #endregion
    }
}