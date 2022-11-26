using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FP_Product_API.Models
{
    public class MostExpensiveCheapestProduct
    {
        public IEnumerable<ProductData> MostExpensiveProduct { get; set; }
        public IEnumerable<ProductData> CheapestProduct { get; set; }

        public MostExpensiveCheapestProduct() 
        {
            MostExpensiveProduct = new List<ProductData>();
            CheapestProduct = new List<ProductData>();
        }
    }
}
