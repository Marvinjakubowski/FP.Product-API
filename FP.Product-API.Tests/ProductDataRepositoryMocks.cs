using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using FP_Product_API.Serializer;
using FP_Product_API.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FP.Product_API.Tests
{
    public static class ProductDataRepositoryMocks
    {
        public static Mock<IProductDataRepository> GetProductDataRepositroryMock()
        {
            var mock = new Mock<IProductDataRepository>(MockBehavior.Loose);

            mock.Setup(_ => _.GetData(
                It.IsAny<string>())).Returns(TestData.GetDataMock());

            return mock;
        }

        public static Mock<IProductDataRepository> SingleArticleGetProductDataRepositroryMock()
        {
            var mock = new Mock<IProductDataRepository>(MockBehavior.Loose);

            mock.Setup(_ => _.GetData(
                It.IsAny<string>())).Returns(TestData.MostExpensiveCheapestProductSingleArticleDataMock());

            return mock;
        }
    }
}
