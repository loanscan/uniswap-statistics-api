using System;

namespace Uniswap.Data.Entities
{
    public interface IExchangeChartEntity : IEntityBase<DateTime>
    {
        decimal EthLiquidity { get; set; }
        decimal TokenLiquidity { get; set; }
        decimal EthVolume { get; set; }
    }
}