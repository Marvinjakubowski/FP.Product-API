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
        private readonly ILogger<ProductStatisticController> _logger;
        private readonly IProductStatisticService _productStatisticService;

        public ProductStatisticController(
            ILogger<ProductStatisticController> logger,
            IProductStatisticService productStatisticService)
        {
            _logger = logger;
            _productStatisticService = productStatisticService;
        }

        [HttpGet("ProductData/GetStatistics")]
        public ProductStatistic GetStatistics([FromBody] IEnumerable<ProductData> data)
        {
            return _productStatisticService.GetProductStatistic(data);
        }
    }
}
