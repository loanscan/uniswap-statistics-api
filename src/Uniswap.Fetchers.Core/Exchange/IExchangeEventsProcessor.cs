using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Contracts;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeEventsProcessor
    {
        Task ProcessAsync(IEnumerable<IEventLog> events);
    }
}