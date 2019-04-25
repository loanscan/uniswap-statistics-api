using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Uniswap.SmartContracts.Exchange.CQS;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeEventsFetcher
    {
        private readonly IEventsFetcherFactory _eventsFetcherFactory;

        public ExchangeEventsFetcher(
            string address,
            Func<string, IEventsFetcherFactory> eventsFetcherFactoryFactory)
        {
            _eventsFetcherFactory = eventsFetcherFactoryFactory(address);
        }

        public async Task<IList<IEventLog>> FetchAsync(ulong fromBlock, ulong toBlock)
        {
            var result = new List<IEventLog>();

            var tokenPurchaseFetcher = _eventsFetcherFactory.Create<TokenPurchaseEventDTO>();
            result.AddRange(await FetchInternalAsync(tokenPurchaseFetcher, fromBlock, toBlock));
            
            var ethPurchaseFetcher = _eventsFetcherFactory.Create<EthPurchaseEventDTO>();
            result.AddRange(await FetchInternalAsync(ethPurchaseFetcher, fromBlock, toBlock));
            
            var addLiquidityFetcher = _eventsFetcherFactory.Create<AddLiquidityEventDTO>();
            result.AddRange(await FetchInternalAsync(addLiquidityFetcher, fromBlock, toBlock));
            
            var removeLiquidityFetcher = _eventsFetcherFactory.Create<RemoveLiquidityEventDTO>();
            result.AddRange(await FetchInternalAsync(removeLiquidityFetcher, fromBlock, toBlock));

            return result;
        }

        private async Task<IList<EventLog<TEventDto>>> FetchInternalAsync<TEventDto>(IEventsFetcher<TEventDto> fetcher,
            ulong fromBlock, ulong toBlock)
            where TEventDto : IEventDTO, new()
        {
            var events = await fetcher.FetchAsync(fromBlock, toBlock);

            return events;
        }
    }
}