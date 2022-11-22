using System.Collections.Generic;
using FP_Product_API.Models;

namespace FP_Product_API.Interfaces.Services
{
    public interface IProductStatisticService
    {
        ProductStatistic GetProductStatistic(IEnumerable<ProductData> data);
    }
}
