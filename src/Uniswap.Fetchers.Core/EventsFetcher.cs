using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Uniswap.Fetchers.Core.Infra;

namespace Uniswap.Fetchers.Core
{
    public class EventsFetcher<TEventDto> : IEventsFetcher<TEventDto>
        where TEventDto : IEventDTO, new()
    {
        private readonly IRetrier _retrier;
        private readonly Event<TEventDto> _event;

        public EventsFetcher(
            IRetrier retrier,
            Event<TEventDto> @event)
        {
            _retrier = retrier;
            _event = @event;
        }

        public async Task<IList<EventLog<TEventDto>>> FetchAsync(ulong fromBlock, ulong toBlock)
        {
            var filter = _event.CreateFilterInput(new BlockParameter(fromBlock), new BlockParameter(toBlock));

            return await _retrier.ExecuteAsync(() => _event.GetAllChanges(filter));
        }
    }
}