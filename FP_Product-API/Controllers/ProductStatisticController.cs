using System.Collections.Generic;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FP_Product_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductStatisticController : ControllerBase
    {
        private readonly IProductStatisticService _productStatisticService;

        public ProductStatisticController(
            IProductStatisticService productStatisticService)
        {
            _productStatisticService = productStatisticService;
        }
        #region HttpGet

        /// <summary>
        /// Gets the following statistics: MostBootles, MostExpensiveAndCheapeastProduct, ProductDataByDefaultPrice, 
        /// Data is provided from: "https://flapotest.blob.core.windows.net/test/ProductData.json" or a custom url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("ProductData/GetStatistics")]
        public ProductStatistic GetStatistics(string? url)
        {
            return _productStatisticService.GetProductStatistic(url);
        }
        #endregion

        #region HttpPost

        /// <summary>
        /// Gets the following statistics: MostBootles, MostExpensiveAndCheapeastProduct, ProductDataByDefaultPrice, 
        /// Data is provided from: "https://flapotest.blob.core.windows.net/test/ProductData.json" or a custom json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("ProductData/GetStatistics")]
        public ProductStatistic GetStatistics([FromBody] IEnumerable<ProductData> data)
        {
            return _productStatisticService.GetProductStatistic(data);
        }
        #endregion

    }
}
