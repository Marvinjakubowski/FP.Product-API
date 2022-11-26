using System.Collections;
using System.Collections.Generic;
using FP_Product_API.Models;

namespace FP_Product_API.Interfaces.Base
{
    public interface IBaseService<T>
    {
        T? Get(int id, string? url);
        T? Get(int id, IEnumerable<ProductData>? data);
        IEnumerable<T>? GetData(string? url);
    }
}
