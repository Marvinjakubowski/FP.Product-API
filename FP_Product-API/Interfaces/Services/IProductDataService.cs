using System.Collections;
using System.Collections.Generic;
using FP_Product_API.Interfaces.Base;
using FP_Product_API.Models;

namespace FP_Product_API.Interfaces.Services
{
    public interface IProductDataService : IBaseService<ProductData>
    {
        IEnumerable<ProductData> SearchProductsByPrice(double Price, IEnumerable<ProductData> data);
        IEnumerable<ProductData> SearchProductsByPrice(double Price, string? url);
        IEnumerable<ProductData> SearchProductsByDefaultPrice(IEnumerable<ProductData> data);
        IEnumerable<ProductData> SearchProductsByDefaultPrice(string? url);
        MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(IEnumerable<ProductData> data);
        MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(string? url);
        IEnumerable<ProductData> MostBottles(IEnumerable<ProductData> data);
        IEnumerable<ProductData> MostBottles(string? url);
    }
}
