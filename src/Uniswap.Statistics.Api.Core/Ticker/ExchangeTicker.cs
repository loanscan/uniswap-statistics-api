namespace Uniswap.Statistics.Api.Core.Ticker
{
    public class ExchangeTicker
    {
        public string Symbol { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public decimal Price { get; set; }
        public decimal InvPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal WeightedAvgPrice { get; set; }
        public decimal PriceChange { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal EthLiquidity { get; set; }
        public decimal Erc20Liquidity { get; set; }
        public decimal LastTradePrice { get; set; }
        public decimal LastTradeEthQty { get; set; }
        public decimal LastTradeErc20Qty { get; set; }
        public decimal TradeVolume { get; set; }
        public long Count { get; set; }
        public string Theme { get; set; }
    }
}