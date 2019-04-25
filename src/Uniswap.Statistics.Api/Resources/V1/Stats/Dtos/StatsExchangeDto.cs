using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Stats.Dtos
{
    public class StatsExchangeDto
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

        [JsonProperty("ethLiquidity")]
        public decimal EthLiquidity { get; set; }
        
        [JsonProperty("erc20Liquidity")]
        public decimal Erc20Liquidity { get; set; }
    }
}