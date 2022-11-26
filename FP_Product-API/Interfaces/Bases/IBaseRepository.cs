using System.Collections;
using System.Collections.Generic;

namespace FP_Product_API.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetData(string? url);
    }
}
