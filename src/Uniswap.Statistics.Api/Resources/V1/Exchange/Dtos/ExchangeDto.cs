using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Exchange.Dtos
{
    public class ExchangeDto
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("exchangeAddress")]
        public string ExchangeAddress { get; set; }
        
        [JsonProperty("ethLiquidity")]
        public decimal EthLiquidity { get; set; }
        
        [JsonProperty("ethDecimal")]
        public int EthDecimals { get; set; }
        
        [JsonProperty("tokenAddress")]
        public string TokenAddress { get; set; }
        
        [JsonProperty("tokenLiquidity")]
        public decimal TokenLiquidity { get; set; }
        
        [JsonProperty("tokenDecimals")]
        public int TokenDecimals { get; set; }
    }
}