using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Price.Dtos
{
    public class PriceDto
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("invPrice")]
        public decimal InvPrice { get; set; }
    }
}