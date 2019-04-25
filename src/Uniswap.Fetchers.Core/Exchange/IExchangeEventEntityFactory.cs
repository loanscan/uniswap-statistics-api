using System;
using Uniswap.Data.Entities;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeEventEntityFactory
    {
        IExchangeEventEntity Create(
            string id,
            string exchangeAddress,
            string callerAddress,
            ExchangeEventType type, 
            decimal ethAmount,
            decimal tokenAmount, 
            decimal ethLiquidityBeforeEvent,
            decimal ethLiquidityAfterEvent,
            decimal tokenLiquidityBeforeEvent,
            decimal tokenLiquidityAfterEvent,
            string txHash,
            int logIndex,
            DateTime timeStamp,
            decimal ethFee, 
            decimal tokenFee,
            string tokenAddress,
            ulong blockNumber,
            decimal callerBalance);
    }
}