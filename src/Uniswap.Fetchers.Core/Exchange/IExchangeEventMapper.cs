using System.Threading.Tasks;
using Nethereum.Contracts;
using Uniswap.Data.Entities;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeEventMapper
    {
        Task<IExchangeEventEntity> MapAsync(IEventLog log, ExchangeState exchangeState, string tokenAddress);
    }
}