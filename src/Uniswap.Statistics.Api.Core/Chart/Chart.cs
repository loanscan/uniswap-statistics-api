using System;

namespace Uniswap.Statistics.Api.Core.Chart
{
    public class Chart
    {
        public DateTime Date { get; set; }
        public decimal EthLiquidity { get; set; }
        public decimal EthVolume { get; set; }
        public decimal MarginalEthRate { get; set; }
        public decimal TokenLiquidity { get; set; }
    }
}