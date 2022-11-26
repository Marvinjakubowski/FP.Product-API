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
        private Mock<ILogger<ProductDataService>> _logger = new Mock<ILogger<ProductDataService>>();
        private Mock<IProductDataRepository> _productDataRepositoryMock = new Mock<IProductDataRepository>();

        [TestMethod]
        public void GetDataTest()
        {
            ProductDataService productDataService = new ProductDataService(_logger.Object, _productDataRepositoryMock.Object);
            var data = productDataService.GetData(null);
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void GetDataById()
        {
            ProductDataService productDataService = new ProductDataService(_logger.Object, _productDataRepositoryMock.Object);
            var data = productDataService.Get(0, String.Empty);
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void GetMostBottles()
        {
            ProductDataService productDataService = new ProductDataService(_logger.Object, _productDataRepositoryMock.Object);
            var data = productDataService.MostBottles(String.Empty);
            Assert.IsNotNull(data);
        }
    }
}