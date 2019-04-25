using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Uniswap.Fetchers.Core
{
    public interface IEventsFetcher<TEventDto>
        where TEventDto : IEventDTO, new()
    {
        Task<IList<EventLog<TEventDto>>> FetchAsync(ulong fromBlock, ulong toBlock);
    }
}