using Uniswap.Data.Entities;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface ILastBlockFetchedByExchangeEntityFactory
    {
        ILastBlockFetchedByExchangeEntity Create(string exchangeAddress, ulong blockNumber);
    }
}