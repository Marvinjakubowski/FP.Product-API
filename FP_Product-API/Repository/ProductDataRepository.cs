using System.Collections.Generic;
using System.Net.Http;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Models;
using FP_Product_API.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FP_Product_API.Serializer
{
    public class ProductDataRepository : IProductDataRepository
    {
        private const string Default_JsonLink = "https://flapotest.blob.core.windows.net/test/ProductData.json";
        private readonly ILogger<ProductDataRepository> _logger;

        public ProductDataRepository(ILogger<ProductDataRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ProductData> GetData(string? Url)
        {
            return GetDefaultDataWithHttpClient(Url);
        }

        private IEnumerable<ProductData> GetDefaultDataWithHttpClient(string? Url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(string.IsNullOrEmpty(Url) ? Default_JsonLink : Url ).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var productData = JsonConvert.DeserializeObject<List<ProductData>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                    if (productData == null)
                    {
                        _logger.LogInformation(
                            "Deserialization was not sucessfull. Message:{ReasonPhrase}",
                            response.ReasonPhrase);

                        return new List<ProductData>();
                    }
                    _logger.LogInformation(
                        "Request was sucessfull. Code:{StatusCode} Message:{ReasonPhrase}",
                         response.StatusCode, response.ReasonPhrase);

                    return productData;
                }
                else
                {
                    _logger.LogWarning("Code:{StatusCode} Message:{ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
                }

            }
            return new List<ProductData>();
        }
    }
}
