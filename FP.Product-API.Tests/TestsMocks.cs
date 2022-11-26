using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Models;
using Moq;

namespace FP.Product_API.Tests
{
    public static class TestsMocks
    {
        public static Mock<IProductDataRepository> GetProductDataRepositroryMock()
        {
            var mock = new Mock<IProductDataRepository>();

            mock.Setup(_ => _.GetData(
                It.IsAny<string>())).Returns(new List<ProductData>());

            return new Mock<IProductDataRepository>();
        }
    }
}
