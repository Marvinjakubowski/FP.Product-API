using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FP_Product_API.Services
{
    public class ProductDataService : IProductDataService
    {
        private const double Default_Price = 17.99;
        private readonly ILogger<ProductDataService> _logger;
        private IProductDataRepository _productDataRepository;

        public ProductDataService(
            ILogger<ProductDataService> logger,
            IProductDataRepository productDataRepository)
        {
            _productDataRepository = productDataRepository;
            _logger = logger;
        }
        public ProductData? Get(int id, IEnumerable<ProductData>? data)
        {
           var defaultdata = _productDataRepository.GetData(null);
           return defaultdata.FirstOrDefault(x => x.Id == id);
        }
        public ProductData? Get(int id, string? url)
        {
            var defaultdata = _productDataRepository.GetData(null);
            return defaultdata.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ProductData> GetObjectData(string? url)
        {
            return _productDataRepository.GetData(url);
        }
        private IEnumerable<ProductData> GetDataToUse(IEnumerable<ProductData>? data)
        {
            return data ?? GetObjectData(null);
        }
        public IEnumerable<ProductData> MostBottles(IEnumerable<ProductData>? data = null)
        {
            var usedData = GetDataToUse(data);
            List<Article> articles = new List<Article>();
            if (usedData?.Count() > 0)
            {
                usedData.ToList().ForEach(x => articles.AddRange(x.Articles));
                if (articles?.Count() > 0)
                {
                    articles = QuickSort(articles, nameof(Article.BottleCount));

                    var articlesWithMostBottles = articles.Where(article => article.BottleCount == articles.Last().BottleCount).ToList();
                    return GetProductDataWithMultipleArticles(usedData, articlesWithMostBottles);
                }
                throw CreateLogHttpExpeption(
                    "No article were found in the given data, request is aborted",
                    HttpStatusCode.BadRequest);
            }
            throw CreateLogHttpExpeption(
               "No ProductData were found in the given data, request is aborted",
               HttpStatusCode.BadRequest);
        }
        public IEnumerable<ProductData> MostBottles(string? url)
        {
            var usedData = GetObjectData(url);
            return MostBottles(usedData);
        }
        public MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(string? url)
        {
            var usedData = GetObjectData(url);
            return GetMostExpensiveCheapestProduct(usedData);
        }
        public MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(IEnumerable<ProductData>? data = null)
        {
            var usedData = GetDataToUse(data);
            List<Article> articles = new List<Article>();
            if (usedData?.Count() > 0)
            {
                usedData.ToList().ForEach(x => articles.AddRange(x.Articles));
                if (articles?.Count() > 0)
                {
                    articles = QuickSort(articles, nameof(Article.Price));
                    return new MostExpensiveCheapestProduct
                    {
                        MostExpensiveProduct = GetProductDataWithMultipleArticles(
                            usedData,
                            articles.Where(article => article.Price == articles.Last().Price).ToList()),
                        CheapestProduct = GetProductDataWithMultipleArticles(
                            usedData,
                            articles.Where(article => article.Price == articles.First().Price).ToList())
                    };
                }
                throw CreateLogHttpExpeption(
                    "No article were found in the given data, request is aborted",
                    HttpStatusCode.BadRequest);
            }
            throw CreateLogHttpExpeption(
                "No entries for articles were found in the given data, request is aborted",
                HttpStatusCode.BadRequest);
        }
        private ProductData GetProductDataWithSingleArticle (IEnumerable<ProductData> data, Article article)
        {
            ProductData productData = data.First(x => x.Articles.Any(y => y.Id == article.Id));
            return new ProductData
            {
                Articles = productData.Articles.Where(x => x.Id == article.Id).ToList(),
                BrandName = productData.BrandName,
                Id = productData.Id,
                Name = productData.Name,
            };
        }
        private IEnumerable<ProductData> GetProductDataWithMultipleArticles(IEnumerable<ProductData> data, List<Article> articles)
        {
            var productDataResult = new List<ProductData>();
            foreach(Article article in articles)
            {
                var productData = GetProductDataWithSingleArticle(data, article);
                if (productDataResult.Where(product => product.Id == productData.Id).Any())
                {
                    productDataResult.First(product => product.Id == productData.Id).Articles.Add(article);
                }
                else
                {
                    productDataResult.Add(productData);
                }
            }
            return productDataResult;
        }
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(IEnumerable<ProductData>? data = null)
        {
            return SearchProductsByPrice(Default_Price, GetDataToUse(data));
        }
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(string? url)
        {
            var usedData = GetObjectData(url);
            return SearchProductsByDefaultPrice(usedData);
        }
        public IEnumerable<ProductData> SearchProductsByPrice(double Price, IEnumerable<ProductData>? data = null )
        {
            var UsedData = GetDataToUse(data);
            List<ProductData> productdata = new List<ProductData>();
                UsedData.Where(x => x.Articles.Any(y => y.Price == Price)).ToList();

            List<Article>  articles= new List<Article>();
            UsedData.Where(x => x.Articles.Any(y => y.Price == Price))
                .ToList()
                .ForEach(product => articles.AddRange(product.Articles));

            articles.ForEach(article => productdata.Add(GetProductDataWithSingleArticle(UsedData, article)));

            productdata = productdata.OrderBy(product => product.Articles.Min(article => article.PricePerUnitDouble)).ToList();

            return productdata;
        }
        public IEnumerable<ProductData> SearchProductsByPrice(double Price, string? url)
        {
            var usedData = GetObjectData(url);
            return SearchProductsByPrice(Price, usedData);
        }
        private List<Article> QuickSort(List<Article> articles, string propertyString)
        {
            var property = typeof(Article).GetProperty(propertyString);
            if (property == null ) 
            { 
                throw CreateLogHttpExpeption(
                    $"No property found with the name {propertyString}",
                    HttpStatusCode.InternalServerError); 
            }

            if (articles.Count <= 1) return articles;

            int pivotPosition = articles.Count / 2;
            double pivotValue = articles[pivotPosition].Price;
            Article pivotArticle = articles[pivotPosition];

            articles.RemoveAt(pivotPosition);

            List<Article> smaller = new List<Article>();
            List<Article> greater = new List<Article>();

            foreach (Article article in articles)
            {
                string? propertyvalue = property.GetValue(article, null)?.ToString();
                if (double.TryParse(propertyvalue, out double value))
                {
                    if (value < pivotValue)
                    {
                        smaller.Add(article);
                    }
                    else
                    {
                        greater.Add(article);
                    }
                }
                else
                {
                    throw CreateLogHttpExpeption(
                        $"Could not parse the following value to double value:{propertyvalue} ArticleId:{article.Id} , Quicksort arborted.",
                        HttpStatusCode.BadRequest);
                }
            }

            List<Article> sorted = QuickSort(smaller, propertyString);
            sorted.Add(pivotArticle);
            sorted.AddRange(QuickSort(greater, propertyString));

            return sorted;
        }
        private HttpRequestException CreateLogHttpExpeption(string message, HttpStatusCode code)
        {
            var httpException = new HttpRequestException(message, null, code);
            _logger.LogError(httpException, httpException.Message);
            return httpException;
        }
    }
}
