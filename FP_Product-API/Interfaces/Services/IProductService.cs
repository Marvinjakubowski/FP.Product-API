using System.Collections;
using System.Collections.Generic;
using FP_Product_API.Interfaces.Base;
using FP_Product_API.Models;

namespace FP_Product_API.Interfaces.Services
{
    public interface IProductService : IBaseService<ProductData>
    {
        IEnumerable<ProductData> GetDefaultProductData();
        IEnumerable<ProductData> SearchProductsByPrice(IEnumerable<ProductData> data, double Price);
        IEnumerable<ProductData> SearchProductsByDefaultPrice(IEnumerable<ProductData> data);
        MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(IEnumerable<ProductData> data);
        ProductData MostBottles(IEnumerable<ProductData> data);
    }
}
