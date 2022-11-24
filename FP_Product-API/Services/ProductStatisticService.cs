using System.Collections.Generic;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;

namespace FP_Product_API.Services
{
    public class ProductStatisticService : IProductStatisticService
    {
        private readonly IProductService _productService;

        public ProductStatisticService(IProductService productService)
        {
            _productService = productService;
        }

        public ProductStatistic GetProductStatistic(IEnumerable<ProductData> data)
        {
            ProductStatistic statistic = new ProductStatistic();
            statistic.ProductWithMostBottles = _productService.MostBottles(data);
            statistic.MostExpensiveCheapestProduct = _productService.GetMostExpensiveCheapestProduct(data);
            statistic.ProductsByDefaultPrice = _productService.SearchProductsByDefaultPrice(data);
            return statistic;
        }
    }
}
