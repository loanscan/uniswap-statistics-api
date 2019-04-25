using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Fetchers.Core.Exchange;

namespace Uniswap.Fetchers.Infrastructure.Mongo.Exchange
{
    public class MongoLastBlockFetchedByExchangeEntityFactory : ILastBlockFetchedByExchangeEntityFactory
    {
        public ILastBlockFetchedByExchangeEntity Create(string exchangeAddress, ulong blockNumber)
        {
            return new MongoLastBlockHandledByExchangeEntity
            {
                Id = exchangeAddress,
                LastBlock = blockNumber
            };
        }
    }
}