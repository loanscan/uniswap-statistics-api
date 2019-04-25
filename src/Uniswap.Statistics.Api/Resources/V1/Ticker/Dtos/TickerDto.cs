using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Ticker.Dtos
{
    public class TickerDto
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("startTime")]
        public long StartTime { get; set; }
        
        [JsonProperty("endTime")]
        public long EndTime { get; set; }
        
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("invPrice")]
        public decimal InvPrice { get; set; }
        
        [JsonProperty("highPrice")]
        public decimal HighPrice { get; set; }
        
        [JsonProperty("lowPrice")]
        public decimal LowPrice { get; set; }
        
        [JsonProperty("weightedAvgPrice")]
        public decimal WeightedAvgPrice { get; set; }
        
        [JsonProperty("priceChange")]
        public decimal PriceChange { get; set; }
        
        [JsonProperty("priceChangePercent")]
        public decimal PriceChangePercent { get; set; }
        
        [JsonProperty("ethLiquidity")]
        public decimal EthLiquidity { get; set; }
        
        [JsonProperty("erc20Liquidity")]
        public decimal Erc20Liquidity { get; set; }
        
        [JsonProperty("lastTradePrice")]
        public decimal LastTradePrice { get; set; }
        
        [JsonProperty("lastTradeEthQty")]
        public decimal LastTradeEthQty { get; set; }
        
        [JsonProperty("lastTradeErc20Qty")]
        public decimal LastTradeErc20Qty { get; set; }
        
        [JsonProperty("tradeVolume")]
        public decimal TradeVolume { get; set; }
        
        [JsonProperty("count")]
        public long Count { get; set; }
        
        [JsonProperty("theme")]
        public string Theme { get; set; }
    }
}