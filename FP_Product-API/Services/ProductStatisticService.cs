using System.Collections.Generic;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using FP_Product_API.Serializer;

namespace FP_Product_API.Services
{
    public class ProductStatisticService : IProductStatisticService
    {
        private readonly IProductDataService _productService;
        private readonly IProductDataRepository _productDataRepository;

        public ProductStatisticService(IProductDataService productService,
            IProductDataRepository productDataRepository)
        {
            _productDataRepository = productDataRepository;
            _productService = productService;
        }

        public ProductStatistic GetProductStatistic(IEnumerable<ProductData> data)
        {
            ProductStatistic statistic = new ProductStatistic
            {
                ProductWithMostBottles = _productService.MostBottles(data),
                MostExpensiveCheapestProduct = _productService.GetMostExpensiveCheapestProduct(data),
                ProductsByDefaultPrice = _productService.SearchProductsByDefaultPrice(data)
            };
            return statistic;
        }

        public ProductStatistic GetProductStatistic(string? url)
        {
            var usedData = _productDataRepository.GetData(url);
            return GetProductStatistic(usedData);
        }
    }
}
