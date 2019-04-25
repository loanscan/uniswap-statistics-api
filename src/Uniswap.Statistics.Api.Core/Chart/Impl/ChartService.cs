using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Chart.Impl
{
    public class ChartService : IChartService
    {
        private class Interval
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        
        private readonly IExchangeEventsRepository _exchangeEventsRepository;

        public ChartService(IExchangeEventsRepository exchangeEventsRepository)
        {
            _exchangeEventsRepository = exchangeEventsRepository;
        }

        public async Task<IEnumerable<Chart>> GetChartData(string exchangeAddress, long startTime, long endTime, ChartUnit unit)
        {
            var start = startTime.ToUnixUtcDateTime();
            var end = endTime.ToUnixUtcDateTime();
            var events = await _exchangeEventsRepository.GetByDateRangeAsync(exchangeAddress, start, end);

            var intervals = GetGraphIntervals(start, end, TimeSpan.FromDays((int)unit));

            return intervals.Aggregate(new List<Chart>(), (list, interval) =>
            {
                var eventsByInterval = events.Where(@event =>
                    @event.Timestamp > interval.Start && @event.Timestamp <= interval.End).OrderBy(x => x.Timestamp).ThenBy(x => x.LogIndex);
                
                if (!eventsByInterval.Any())
                {
                    var lastElement = list.LastOrDefault();
                    list.Add(new Chart
                    {
                        Date = interval.Start.Date,
                        EthLiquidity = lastElement?.EthLiquidity ?? 0,
                        EthVolume = 0,
                        TokenLiquidity = lastElement?.TokenLiquidity ?? 0,
                        MarginalEthRate = lastElement?.MarginalEthRate ?? 0
                    });
                    return list;
                }

                var lastEventInInterval = eventsByInterval.Last();
                var date = interval.Start.Date;
                var ethLiquidity = lastEventInInterval.EthLiquidityAfterEvent;
                var tokenLiquidity = lastEventInInterval.TokenLiquidityAfterEvent;
                var ethVolume = eventsByInterval.Where(IsEthOrTokenPurchaseEvent).Sum(x => x.EthAmount);
                
                list.Add(new Chart
                {
                    Date = date,
                    EthLiquidity = ethLiquidity,
                    EthVolume = ethVolume,
                    TokenLiquidity = tokenLiquidity,
                    MarginalEthRate = tokenLiquidity / ethLiquidity
                });
                return list;
            });
        }

        private IEnumerable<Interval> GetGraphIntervals(DateTime startDateTime, DateTime endDateTime, TimeSpan intervalDuration)
        {
            if (endDateTime - startDateTime < intervalDuration)
            {
                return new List<Interval>
                {
                    new Interval
                    {
                        Start = startDateTime,
                        End = endDateTime
                    }
                };
            }

            var result = new List<Interval>();
            var interval = new Interval
            {
                Start = startDateTime,
                End = startDateTime.Date.Add(intervalDuration)
            };
            result.Add(interval);

            var flag = interval.End;
            while (flag.Add(intervalDuration) < endDateTime)
            {
                var currentInterval = new Interval
                {
                    Start = result.Last().End,
                    End = result.Last().End.Add(intervalDuration)
                };
                result.Add(currentInterval);
                flag = currentInterval.End;
            }
            result.Add(new Interval
            {
                Start = result.Last().End,
                End = endDateTime
            });
            return result;
        }

        private bool IsEthOrTokenPurchaseEvent(IExchangeEventEntity entity)
        {
            return entity.Type == ExchangeEventType.EthPurchase || entity.Type == ExchangeEventType.TokenPurchase;
        }
    }
}