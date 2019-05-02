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

            var ticker = new ExchangeTicker();
            if (orderedPurchaseEvents.Any())
            {
                var firstTrade = orderedPurchaseEvents.First();

                var lastTrade = orderedPurchaseEvents.Last();

                var startPrice = firstTrade.TokenAmount / firstTrade.EthAmount;

                var priceChange = UniswapUtils.CalculateMarginalRate(lastTrade.EthLiquidityBeforeEvent,
                                      lastTrade.TokenLiquidityBeforeEvent) -
                                  UniswapUtils.CalculateMarginalRate(firstTrade.EthLiquidityBeforeEvent,
                                      firstTrade.TokenLiquidityBeforeEvent);

                var ethTradeVolume = orderedPurchaseEvents.Sum(@event => @event.EthAmount);

                var highPrice = orderedPurchaseEvents.Max(@event =>
                    UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                        @event.TokenLiquidityBeforeEvent));

                var lowPrice = orderedPurchaseEvents.Min(@event =>
                    UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                        @event.TokenLiquidityBeforeEvent));

                var weightedAveragePrice =
                    orderedPurchaseEvents.Sum(@event =>
                        @event.EthAmount *
                        UniswapUtils.CalculateMarginalRate(@event.EthLiquidityBeforeEvent,
                            @event.TokenLiquidityBeforeEvent)) / ethTradeVolume;

                ticker.Count = orderedPurchaseEvents.Count();
                ticker.LowPrice = lowPrice;
                ticker.HighPrice = highPrice;
                ticker.LastTradeErc20Qty = lastTrade.TokenAmount;
                ticker.LastTradeEthQty = lastTrade.EthAmount;
                ticker.LastTradePrice = UniswapUtils.CalculateMarginalRate(lastTrade.EthLiquidityBeforeEvent,
                    lastTrade.TokenLiquidityBeforeEvent);
                ticker.PriceChange = priceChange;
                ticker.TradeVolume = ethTradeVolume;
                ticker.PriceChangePercent = priceChange / startPrice;
                ticker.WeightedAvgPrice = weightedAveragePrice;
            }

            ticker.EndTime = DateTime.UtcNow.ToUnixTimestamp();
            ticker.Erc20Liquidity = exchangeEntity.TokenLiquidity;
            ticker.EthLiquidity = exchangeEntity.EthLiquidity;
            ticker.Price =
                UniswapUtils.CalculateMarginalRate(exchangeEntity.EthLiquidity, exchangeEntity.TokenLiquidity);
            ticker.InvPrice =
                UniswapUtils.CalculateInvMarginRate(exchangeEntity.EthLiquidity, exchangeEntity.TokenLiquidity);
            ticker.Symbol = exchangeEntity.TokenSymbol;
            ticker.StartTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)).ToUnixTimestamp();
            ticker.Theme = exchangeEntity.Theme;
            
            return new OperationResult<ExchangeTicker>(ticker);
        }

        private bool IsEthOrTokenPurchaseEvent(IExchangeEventEntity entity)
        {
            return entity.Type == ExchangeEventType.EthPurchase || entity.Type == ExchangeEventType.TokenPurchase;
        }
    }
}