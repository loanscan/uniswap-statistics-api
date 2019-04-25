using System;
using System.Linq;
using System.Threading.Tasks;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Ticker.Impl
{
    public class TickerService : ITickerService
    {
        private readonly IExchangeEventsRepository _repository;
        private readonly IExchangeRepository _exchangeRepository;

        public TickerService(IExchangeEventsRepository repository, IExchangeRepository exchangeRepository)
        {
            _repository = repository;
            _exchangeRepository = exchangeRepository;
        }

        public async Task<OperationResult<ExchangeTicker>> GetByAddress(string exchangeAddress)
        {
            var exchangeEntity = await _exchangeRepository.FindByAsync(exchangeAddress: exchangeAddress);

            if (exchangeEntity == null)
            {
                return new OperationResult<ExchangeTicker>();
            }

            var exchangeEvents = await _repository.GetForLastDayByExchangeAddressAsync(exchangeAddress);
            
            if (!exchangeEvents.Any())
            {
                return new OperationResult<ExchangeTicker>(null);
            }

            var orderedPurchaseEvents = exchangeEvents
                .Where(IsEthOrTokenPurchaseEvent)
                .OrderBy(x => x.BlockNumber)
                .ThenBy(x => x.LogIndex);
            
            var firstTrade = orderedPurchaseEvents.First();

            var lastTrade = orderedPurchaseEvents.Last();

            var startPrice = firstTrade.TokenAmount / firstTrade.EthAmount;

            var priceChange = UniswapUtils.CalculateMarginalRate(lastTrade.EthLiquidityBeforeEvent, lastTrade.TokenLiquidityBeforeEvent) -
                              UniswapUtils.CalculateMarginalRate(firstTrade.EthLiquidityBeforeEvent, firstTrade.TokenLiquidityBeforeEvent);

            var ethTradeVolume = orderedPurchaseEvents.Sum(@event => @event.EthAmount);

            var highPrice = orderedPurchaseEvents.Max(@event =>
                UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                    @event.TokenLiquidityBeforeEvent));

            var lowPrice = orderedPurchaseEvents.Min(@event =>
                UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                    @event.TokenLiquidityBeforeEvent));

            var weightedAveragePrice = orderedPurchaseEvents.Sum(@event =>
                                           @event.EthAmount *
                                           UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                                               @event.TokenLiquidityBeforeEvent)) / ethTradeVolume;
            
            var ticker = new ExchangeTicker
            {
                Count = orderedPurchaseEvents.Count(),
                EndTime = DateTime.UtcNow.ToUnixTimestamp(),
                Erc20Liquidity = exchangeEntity.TokenLiquidity,
                EthLiquidity = exchangeEntity.EthLiquidity,
                LowPrice = lowPrice,
                HighPrice = highPrice,
                Price = UniswapUtils.CalculateMarginalRate(exchangeEntity.EthLiquidity, exchangeEntity.TokenLiquidity),
                InvPrice = UniswapUtils.CalculateInvMarginRate(exchangeEntity.EthLiquidity, exchangeEntity.TokenLiquidity),
                LastTradeErc20Qty = lastTrade.TokenAmount,
                LastTradeEthQty = lastTrade.EthAmount,
                LastTradePrice = UniswapUtils.CalculateMarginalRate(lastTrade.EthLiquidityBeforeEvent, lastTrade.TokenLiquidityBeforeEvent),
                Symbol = exchangeEntity.TokenSymbol,
                PriceChange = priceChange,
                StartTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)).ToUnixTimestamp(),
                TradeVolume = ethTradeVolume,
                PriceChangePercent = priceChange / startPrice,
                WeightedAvgPrice = weightedAveragePrice,
                Theme = exchangeEntity.Theme
            };
            
            return new OperationResult<ExchangeTicker>(ticker);
        }

        private bool IsEthOrTokenPurchaseEvent(IExchangeEventEntity entity)
        {
            return entity.Type == ExchangeEventType.EthPurchase || entity.Type == ExchangeEventType.TokenPurchase;
        }
    }
}