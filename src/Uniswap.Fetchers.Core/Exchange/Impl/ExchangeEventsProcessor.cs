using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nethereum.Contracts;
using Uniswap.Common;
using Uniswap.Data.Repositories;
using Uniswap.Fetchers.Core.Eth;
using Uniswap.Fetchers.Core.Token;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeEventsProcessor : IExchangeEventsProcessor
    {
        private readonly ITokenLiquidityProvider _tokenLiquidityProvider;
        private readonly IEthLiquidityProvider _ethLiquidityProvider;
        private readonly Func<string, IExchangeGateway> _exchangeGatewayFactory;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeEventsRepository _exchangeEventsRepository;
        private readonly IExchangeEventMapper _exchangeEventMapper;
        private readonly IExchangeTotalSupplyProvider _exchangeTotalSupplyProvider;
        private readonly ILogger<ExchangeEventsProcessor> _logger;

        public ExchangeEventsProcessor(
            ITokenLiquidityProvider tokenLiquidityProvider,
            IEthLiquidityProvider ethLiquidityProvider,
            Func<string, IExchangeGateway> exchangeGatewayFactory,
            IExchangeRepository exchangeRepository,
            IExchangeEventsRepository exchangeEventsRepository,
            IExchangeEventMapper exchangeEventMapper,
            IExchangeTotalSupplyProvider exchangeTotalSupplyProvider,
            ILogger<ExchangeEventsProcessor> logger)
        {
            _tokenLiquidityProvider = tokenLiquidityProvider;
            _ethLiquidityProvider = ethLiquidityProvider;
            _exchangeGatewayFactory = exchangeGatewayFactory;
            _exchangeRepository = exchangeRepository;
            _exchangeEventsRepository = exchangeEventsRepository;
            _exchangeEventMapper = exchangeEventMapper;
            _exchangeTotalSupplyProvider = exchangeTotalSupplyProvider;
            _logger = logger;
        }

        public async Task ProcessAsync(IEnumerable<IEventLog> events)
        {
            var groupedByExchangeEvents = events.GroupBy(x => x.Log.Address);

            foreach (var exchangeEvents in groupedByExchangeEvents)
            {
                var groupedAndOrderedByBlockNumberEvents =
                    exchangeEvents.GroupBy(x => x.Log.BlockNumber).OrderBy(x => x.Key.Value);

                foreach (var orderedBlockEvents in groupedAndOrderedByBlockNumberEvents)
                {
                    var previousBlockNumber = (ulong) orderedBlockEvents.Key.Value - 1;
                    var tokenAddress = await _exchangeGatewayFactory(exchangeEvents.Key).GetTokenAddressAsync();
                    var exchangeState = new ExchangeState()
                    {
                        EthLiquidity = await _ethLiquidityProvider.GetAsync(exchangeEvents.Key,
                            previousBlockNumber),
                        TokenLiquidity = await _tokenLiquidityProvider.GetAsync(tokenAddress,
                            exchangeEvents.Key, previousBlockNumber)
                    };

                    foreach (var eventLog in orderedBlockEvents)
                    {
                        var entity = await _exchangeEventMapper.MapAsync(eventLog, exchangeState, tokenAddress);

                        exchangeState.Update(entity);

                        var totalSupply = await _exchangeTotalSupplyProvider.GetAsync(entity.ExchangeAddress);
                        await _exchangeRepository.Update(entity.ExchangeAddress, entity.EthLiquidityAfterEvent,
                            entity.TokenLiquidityAfterEvent, totalSupply);

                        await _exchangeEventsRepository.AddOrUpdateAsync(entity);

                        _logger.LogInformation("Event txHash {txHash} logIndex {logIndex} was processed",
                            eventLog.Log.TransactionHash, eventLog.Log.LogIndex.Value);
                        _logger.LogInformation("Updated state: {state}", ObjectDumper.Dump(exchangeState));
                    }
                }
            }
        }
    }
}