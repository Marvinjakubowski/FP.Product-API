using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FP_Product_API.Models
{
    public class Article
    {
        public int Id { get; set; }
        [JsonIgnore]
        private string _ShortDescription { get; set; }
        public string ShortDescription 
        {
            get
            {
                return _ShortDescription;
            }
            set
            {
                _ShortDescription = value;
                Regex regex = new Regex(@"\d{1,}[ ]");
                Match match = regex.Match(value);
                if(match.Success)
                {
                    if (int.TryParse(match.Value, out int count))
                    {
                        BottleCount = count;
                    }
                }
            }
        }
        public double Price { get; set; }
        public string? Unit { get; set; }
        [JsonIgnore]
        private string _PricePerUnitText { get; set; }
        public string PricePerUnitText 
        { 
            get
            {
                return _PricePerUnitText;
            }
            set 
            {
                _PricePerUnitText = value;
                Regex regex = new Regex(@"\d{1,6},\d{2}");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    if (double.TryParse(match.Value.Replace(',','.'), out double pricePerUnit))
                    {
                        PricePerUnitDouble = pricePerUnit;
                    }
                }
            } 
        }
        [JsonIgnore]
        public double? PricePerUnitDouble { get; private set; }
        [JsonIgnore]
        public int BottleCount { get; private set; }
        public string? Image { get; set; }
        public Article()
        {
            _PricePerUnitText = "";
            _ShortDescription = "";
        }

    }
}
