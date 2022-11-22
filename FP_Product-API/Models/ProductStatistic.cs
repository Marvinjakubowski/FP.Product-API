using System.Collections.Generic;

namespace FP_Product_API.Models
{
    public class ProductStatistic
    {
        public IEnumerable<ProductData> ProductsByDefaultPrice { get; set; }
        public ProductData? CheapestProduct { get; set; }
        public ProductData? MostExpensiveProduct { get; set; }
        public ProductData? ProductWithMostBottles { get; set; }
 
        public ProductStatistic()
        {
            ProductsByDefaultPrice = new List<ProductData>();
        }

    }
}
