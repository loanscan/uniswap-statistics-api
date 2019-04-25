using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Directory.Dtos
{
    public class DirectoryDto
    {        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("tokenAddress")]
        public string TokenAddress { get; set; }
        
        [JsonProperty("tokenDecimals")]
        public int TokenDecimals { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }
        
        [JsonProperty("exchangeAddress")]
        public string ExchangeAddress { get; set; }
    }
}