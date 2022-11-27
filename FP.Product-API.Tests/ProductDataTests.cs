using System.Security.Cryptography.X509Certificates;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FP.Product_API.Tests
{
    [TestClass]
    public class ProductDataTests
    {
        [TestMethod]
        public void GetDataTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object, 
                TestsMocks.GetProductDataRepositroryMock().Object);
            var data = productDataService.GetObjectData(null);
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetDataByIdTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var id = 2827;
            var data = productDataService.Get(id, string.Empty);

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Id == id);
        }

        [TestMethod]
        public void GetMostBottlesTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var data = productDataService.MostBottles(string.Empty);

            Assert.IsNotNull(data);
        }
        
        [TestMethod]
        public void GetMostExpensiveCheapestProductMultipleResultTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var data = productDataService.GetMostExpensiveCheapestProduct(string.Empty);

            var mostExpensivePrice = 17.99;
            var cheapestPrice = 15.99;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.MostExpensiveProduct.First().Articles.First().Price == mostExpensivePrice);
            Assert.IsTrue(data.CheapestProduct.First().Articles.First().Price == cheapestPrice);
            Assert.IsTrue(data.CheapestProduct.First().Articles.Count > 1);
        }

        [TestMethod]
        public void GetMostExpensiveCheapestProductSingleResultTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.SingleArticleGetProductDataRepositroryMock().Object);
            var data = productDataService.GetMostExpensiveCheapestProduct(string.Empty);

            var mostExpensivePrice = 16.99;
            var cheapestPrice = 15.99;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.MostExpensiveProduct.First().Articles.First().Price == mostExpensivePrice);
            Assert.IsTrue(data.CheapestProduct.First().Articles.First().Price == cheapestPrice);
            Assert.IsTrue(data.MostExpensiveProduct.First().Articles.Count == 1);
            Assert.IsTrue(data.CheapestProduct.First().Articles.Count == 1);
        }

        [TestMethod]
        public void SearchProductsByDefaultPriceTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var data = productDataService.SearchProductsByDefaultPrice(string.Empty);

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void SearchProductsByPriceTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var searchPrice = 15.99;
            var data = productDataService.SearchProductsByPrice(searchPrice, string.Empty);

            Assert.IsNotNull(data);
            Assert.IsTrue(data.First().Articles.First().Price == searchPrice);
        }

        [TestMethod]
        public void SearchProductsByPriceNoResultTest()
        {
            var _logger = new Mock<ILogger<ProductDataService>>();
            ProductDataService productDataService = new ProductDataService(
                _logger.Object,
                TestsMocks.GetProductDataRepositroryMock().Object);
            var searchPrice = 0;
            var data = productDataService.SearchProductsByPrice(searchPrice, string.Empty);

            Assert.IsTrue(data.Count() == 0);
        }
    }
}