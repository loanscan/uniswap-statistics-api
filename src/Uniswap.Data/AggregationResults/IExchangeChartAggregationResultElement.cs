using System;

namespace Uniswap.Data.AggregationResults
{
    public interface IExchangeChartAggregationResultElement
    {
        DateTime Id { get; set; }
        decimal EthLiquidity { get; set; }
        decimal TokenLiquidity { get; set; }
        decimal EthVolume { get; set; }
    }
}