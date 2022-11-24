using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FP_Product_API.Services
{
    public class ProductService : IProductService
    {
        private const double Default_Price = 17.99;
        private const string Default_JsonLink = "https://flapotest.blob.core.windows.net/test/ProductData.json";
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger) {
            _logger = logger;
        }

        public ProductData? Get(int id, IEnumerable<ProductData> data)
        {
           return data.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductData> GetDefaultProductData()
        {
            return GetDefaultDataWithHttpClient();
        }

        public ProductData MostBottles(IEnumerable<ProductData> data)
        {
            throw new System.NotImplementedException();
        }

        public MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(IEnumerable<ProductData> data)
        {
            List<Article> articles = new List<Article>();
            data.ToList().ForEach(x => articles.AddRange(x.Articles));
            articles = QuickSortPrice(articles);
            return new MostExpensiveCheapestProduct
            {
                MostExpensiveProduct = GetProductDataWithSingleArticle(data, articles.First()),
                CheapestProduct = GetProductDataWithSingleArticle(data, articles.Last())
            };
        }

        private ProductData GetProductDataWithSingleArticle (IEnumerable<ProductData> data, Article article)
        {
            ProductData productData = data.First(x => x.Articles.Any(y => y.Id == article.Id));
            productData.Articles = productData.Articles.Where(x => x.Id == article.Id).ToList();
            return productData;
        }

        public IEnumerable<ProductData> SearchProductsByDefaultPrice(IEnumerable<ProductData> data)
        {
           return SearchProductsByPrice(data, Default_Price);
        }

        public IEnumerable<ProductData> SearchProductsByPrice(IEnumerable<ProductData> data, double Price)
        {
            List<ProductData> productdata =
                data.Where(x => x.Articles.Any(y => y.Price == Price)).ToList();

            productdata.ForEach(
                 product => product.Articles = product.Articles.Where(
                 article => article.Price == Price).ToList());

            productdata = productdata.OrderBy(product => product.Articles.Min(article => article.PricePerUnitDouble)).ToList();

            return productdata;
        }

        private IEnumerable<ProductData> GetDefaultDataWithHttpClient()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(Default_JsonLink).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var productData = JsonConvert.DeserializeObject<List<ProductData>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                    if (productData == null)
                    {
                        _logger.LogInformation(
                            "Deserializeatuin was not sucessfull. Message:{Content}",
                            new { response.Content });

                        return new List<ProductData>();
                    }
                    _logger.LogInformation(
                        "Request was sucessfull. Code:{StatusCode} Message:{Content}",
                        new { response.StatusCode, response.Content });

                    return productData;
                }
                else
                {
                    _logger.LogWarning("Code:{StatusCode} Message:{Message}", new { response.StatusCode, response.ReasonPhrase });
                }

            }
            return new List<ProductData>();
        }
        private List<Article> QuickSortPrice(List<Article> articles)
        {
            if (articles.Count <= 1) return articles;
            int pivotPosition = articles.Count / 2;
            double pivotValue = articles[pivotPosition].Price;
            Article pivotArticle = articles[pivotPosition];
            articles.RemoveAt(pivotPosition);
            List<Article> smaller = new List<Article>();
            List<Article> greater = new List<Article>();
            foreach (Article article in articles)
            {
                if (article.Price < pivotValue)
                {
                    smaller.Add(article);
                }
                else
                {
                    greater.Add(article);
                }
            }
            List<Article> sorted = QuickSortPrice(smaller);
            sorted.Add(pivotArticle);
            sorted.AddRange(QuickSortPrice(greater));
            return sorted;
        }

    }
}
