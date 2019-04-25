using System;
using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Fetchers.Core.Exchange;

namespace Uniswap.Fetchers.Infrastructure.Mongo.Exchange
{
    public class MongoExchangeEventEntityFactory : IExchangeEventEntityFactory
    {
        public IExchangeEventEntity Create(
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
            decimal callerBalance)
        {
            return new MongoExchangeEventEntity
            {
                Id = id,
                ExchangeAddress = exchangeAddress,
                CallerAddress = callerAddress,
                Type = type,
                EthAmount = ethAmount,
                TokenAmount = tokenAmount,
                EthLiquidityBeforeEvent = ethLiquidityBeforeEvent,
                EthLiquidityAfterEvent = ethLiquidityAfterEvent,
                TokenLiquidityBeforeEvent = tokenLiquidityBeforeEvent,
                TokenLiquidityAfterEvent = tokenLiquidityAfterEvent,
                TxHash = txHash,
                LogIndex = logIndex,
                Timestamp = timeStamp,
                EthFee = ethFee,
                TokenFee = tokenFee,
                TokenAddress = tokenAddress,
                BlockNumber = blockNumber,
                CallerBalance = callerBalance
            };
        }
    }
}