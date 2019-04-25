using System;

namespace Uniswap.Data.Entities
{
    public interface IExchangeEventEntity : IEntityBase<string>
    {
        string ExchangeAddress { get; set; }
        string CallerAddress { get; set; }
        ExchangeEventType Type { get; set; }
        decimal EthAmount { get; set; }
        decimal TokenAmount { get; set; }
        decimal EthLiquidityBeforeEvent { get; set; }
        decimal EthLiquidityAfterEvent { get; set; }
        decimal TokenLiquidityBeforeEvent { get; set; }
        decimal TokenLiquidityAfterEvent { get; set; }
        string TxHash { get; set; }
        int LogIndex { get; set; }
        DateTime Timestamp { get; set; }
        decimal EthFee { get; set; }
        decimal TokenFee { get; set; }
        string TokenAddress { get; set; }
        ulong BlockNumber { get; set; }
        decimal CallerBalance { get; set; }
    }
}