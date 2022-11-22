using System.Text.Json.Serialization;

namespace FP_Product_API.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? ShortDescription 
        {
            get
            {
                return this.ShortDescription;
            }
            set
            {
                ShortDescription = value;
                int indexOfEnd = this.PricePerUnitText.IndexOf('x');
                if (int.TryParse(PricePerUnitText.Substring(0, (indexOfEnd) - 1), out int count))
                {
                    BottleCount = count;
                }
                else
                {
                    BottleCount = null;
                }
            }
        }
        public double Price { get; set; }
        public string? Unit { get; set; }
        public string PricePerUnitText 
        { 
            get
            {
                return this.PricePerUnitText;
            }
            set 
            {
                PricePerUnitText = value;
                int indexOfStart = 1;
                int indexOfEnd = this.PricePerUnitText.IndexOf('€');
                if(double.TryParse(PricePerUnitText.Substring(indexOfStart,(indexOfEnd)-1), out double pricePerUnit))
                {
                    PricePerUnitDouble = pricePerUnit;
                }
                else
                {
                    PricePerUnitDouble = null;
                }
            } 
        }
        [JsonIgnore]
        public double? PricePerUnitDouble { get; private set; }
        [JsonIgnore]
        public int? BottleCount { get; private set; }

        public string? Image { get; set; }

    }
}
