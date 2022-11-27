using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP_Product_API.Models;

namespace FP.Product_API.Tests
{
    public static class TestData
    {
        private static IEnumerable<ProductData> GetDefaultProductData()
        {
            return new List<ProductData>
            {
                new ProductData
                {
                    Id= 2827,
                    BrandName = "Grevensteiner",
                    Name = "Grevensteiner Helles",
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            Id = 3785,
                            ShortDescription = "20 x 0,33L (Glas)",
                            Price = 15.99,
                            Unit = "Liter",
                            PricePerUnitText = "(2,42 €/Liter)",
                            Image = "https://image.flaschenpost.de/articles/small/3785.png"
                        },

                        new Article
                        {
                            Id = 3831,
                            ShortDescription = "16 x 0,5L (Glas)",
                            Price = 15.99,
                            Unit = "Liter",
                            PricePerUnitText = "(2,00 €/Liter)",
                            Image = "https://image.flaschenpost.de/articles/small/3831.png"
                        },
                    }
                },
                new ProductData
                {
                    Id= 2135,
                    BrandName = "Hofbräu",
                    Name = "Hofbräu Oktoberfestbier",
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            Id = 2834,
                            ShortDescription = "20 x 0,5L (Glas)",
                            Price = 16.99,
                            Unit = "Liter",
                            PricePerUnitText = "(1,70 €/Liter)",
                            Image = "https://image.flaschenpost.de/articles/small/2834.png"
                        },
                         new Article
                        {
                            Id = 2835,
                            ShortDescription = "10 x 0,5L (Glas)",
                            Price = 17.99,
                            Unit = "Liter",
                            PricePerUnitText = "(1,80 €/Liter)",
                            Image = "https://image.flaschenpost.de/articles/small/2834.png"
                        }
                    }
                }
            };
        }

        public static IEnumerable<ProductData> GetDataMock()
        {
            var ProductDataMock = GetDefaultProductData();
            return ProductDataMock;
        }
        public static IEnumerable<ProductData> MostExpensiveCheapestProductSingleArticleDataMock()
        {
            var ProductDataMock = GetDefaultProductData();
            ProductDataMock.ToList().ForEach(x => x.Articles = x.Articles.Take(1).ToList());
            return ProductDataMock;
        }
    }
}
