using System.Collections.Generic;

namespace FP_Product_API.Models
{
    public class ProductStatistic
    {
        public IEnumerable<ProductData> ProductsByDefaultPrice { get; set; }
        public MostExpensiveCheapestProduct MostExpensiveCheapestProduct { get; set; }
        public IEnumerable<ProductData> ProductWithMostBottles { get; set; }
 
        public ProductStatistic()
        {
            ProductsByDefaultPrice = new List<ProductData>();
            MostExpensiveCheapestProduct = new MostExpensiveCheapestProduct();
            ProductWithMostBottles = new List<ProductData>();
        }

    }
}
