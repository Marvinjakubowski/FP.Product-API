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

        public IEnumerable<ProductData> GetData(string? url)
        {
            return _productDataRepository.GetData(url);
        }
        private IEnumerable<ProductData> GetDataToUse(IEnumerable<ProductData>? data)
        {
            return data ?? GetData(null);
        }
        public IEnumerable<ProductData> MostBottles(IEnumerable<ProductData>? data = null)
        {
            var usedData = GetDataToUse(data);
            List<Article> articles = new List<Article>();
            if (usedData != null)
            {
                usedData.ToList().ForEach(x => articles.AddRange(x.Articles));
                articles = QuickSort(articles, nameof(Article.BottleCount));
                var articlesWithMostBottles = articles.Where(article => article.BottleCount == articles.Last().BottleCount).ToList();
                return GetProductDataWithMultipleArticle(usedData, articlesWithMostBottles);
            }
            return new List<ProductData>();
        }
        public IEnumerable<ProductData> MostBottles(string? url)
        {
            var usedData = GetData(url);
            return MostBottles(usedData);
        }
        public MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(IEnumerable<ProductData>? data = null)
        {
            var usedData = GetDataToUse(data);
            List<Article> articles = new List<Article>();
            if (usedData != null)
            {
                usedData.ToList().ForEach(x => articles.AddRange(x.Articles));
                articles = QuickSort(articles, nameof(Article.Price));
                return new MostExpensiveCheapestProduct
                {
                    MostExpensiveProduct = GetProductDataWithMultipleArticle(
                        usedData,
                        articles.Where(article => article.Price == articles.Last().Price).ToList()),
                    CheapestProduct = GetProductDataWithMultipleArticle(
                        usedData,
                        articles.Where(article => article.Price == articles.First().Price).ToList())
                };
            }
            return new MostExpensiveCheapestProduct();
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

        private IEnumerable<ProductData> GetProductDataWithMultipleArticle(IEnumerable<ProductData> data, List<Article> articles)
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
        public MostExpensiveCheapestProduct GetMostExpensiveCheapestProduct(string? url)
        {
            var usedData = GetData(url);
            return GetMostExpensiveCheapestProduct(usedData);
        }
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(IEnumerable<ProductData>? data = null)
        {
            return SearchProductsByPrice(Default_Price, GetDataToUse(data));
        }
        public IEnumerable<ProductData> SearchProductsByDefaultPrice(string? url)
        {
            var usedData = GetData(url);
            return SearchProductsByDefaultPrice(usedData);
        }
        public IEnumerable<ProductData> SearchProductsByPrice(double Price, IEnumerable<ProductData>? data = null )
        {
            var UsedData = GetDataToUse(data);
            List<ProductData> productdata =
                UsedData.Where(x => x.Articles.Any(y => y.Price == Price)).ToList();

            productdata.ForEach(
                 product => product.Articles = product.Articles.Where(
                 article => article.Price == Price).ToList());

            productdata = productdata.OrderBy(product => product.Articles.Min(article => article.PricePerUnitDouble)).ToList();

            return productdata;
        }
        public IEnumerable<ProductData> SearchProductsByPrice(double Price, string? url)
        {
            var usedData = GetData(url);
            return SearchProductsByPrice(Price, usedData);
        }

        private List<Article> QuickSort(List<Article> articles, string propertyString)
        {
            var property = typeof(Article).GetProperty(propertyString);
            if (property == null ) { throw new Exception(); }
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
                    _logger.LogError("Could not parse the following value to double value:{value} ArticleId:{id} , Quicksort arborted.",
                        propertyvalue,
                        article.Id);
                }
            }
            List<Article> sorted = QuickSort(smaller, propertyString);
            sorted.Add(pivotArticle);
            sorted.AddRange(QuickSort(greater, propertyString));
            return sorted;
        }




    }
}
