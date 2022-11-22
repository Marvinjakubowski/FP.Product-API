using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FP_Product_API.Models
{
    public class MostExpensiveCheapestProduct
    {
        public ProductData? MostExpensiveProduct { get; set; }
        public ProductData? CheapestProduct { get; set; }
    }
}
