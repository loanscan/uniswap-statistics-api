using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Stats.Impl
{
    public class StatsService : IStatsService
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeEventsRepository _exchangeEventsRepository;

        public StatsService(
            IExchangeRepository exchangeRepository,
            IExchangeEventsRepository exchangeEventsRepository)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeEventsRepository = exchangeEventsRepository;
        }

        public async Task<IEnumerable<IExchangeEntity>> GetExchangesAsync(StatsOrderBy orderBy, decimal minEthLiquidityAmount = 0)
        {
            var exchanges = (await _exchangeRepository.GetAsync(minEthLiquidityAmount)).ToList();

            switch (orderBy)
            {
                case StatsOrderBy.Liquidity:
                    return exchanges;
                case StatsOrderBy.Volume:
                    var events = (await _exchangeEventsRepository.GetForLastDayAsync()).ToList();
                    var ethVolumes = new Dictionary<string, decimal>();
                    foreach (var exchange in exchanges)
                    {
                        ethVolumes[exchange.Id] = 0;
                    }

                    foreach (var groupedAndOrderedEvent in events.OrderBy(e => e.BlockNumber).ThenBy(e => e.LogIndex)
                        .GroupBy(e => e.ExchangeAddress))
                    {
                        ethVolumes[groupedAndOrderedEvent.Key] = groupedAndOrderedEvent.Sum(e => e.EthAmount);
                    }

                    exchanges.Sort((x, y) => -ethVolumes[x.Id].CompareTo(ethVolumes[y.Id]));
                    return exchanges;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null);
            }
        }
    }
}