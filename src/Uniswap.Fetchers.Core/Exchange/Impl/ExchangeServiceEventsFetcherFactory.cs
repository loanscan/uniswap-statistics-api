using System;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.SmartContracts.Exchange.Service;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeServiceEventsFetcherFactory : IEventsFetcherFactory
    {
        private readonly ExchangeService _exchangeService;
        private readonly IRetrier _retrier;
        
        public ExchangeServiceEventsFetcherFactory(
            string address,
            Func<string, ExchangeService> exchangeServiceFactory,
            IRetrier retrier)
        {
            _exchangeService = exchangeServiceFactory(address);
            _retrier = retrier;
        }
        
        public IEventsFetcher<TEventDto> Create<TEventDto>() where TEventDto : IEventDTO, new()
        {
            return new EventsFetcher<TEventDto>(_retrier, _exchangeService.ContractHandler.GetEvent<TEventDto>());
        }
    }
}