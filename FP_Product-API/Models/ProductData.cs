using System.Collections.Generic;

namespace FP_Product_API.Models
{
    public class ProductData
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; }

        public ProductData()
        {
            BrandName = "";
            Name = "";
            Articles = new List<Article>();
        }
    }
}