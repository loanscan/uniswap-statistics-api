using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.AggregationResults;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Repositories
{
    public interface IExchangeEventsRepository
    {
        Task AddOrUpdateAsync(IExchangeEventEntity entity);
        Task<IEnumerable<IExchangeEventEntity>> GetForLastDayAsync();
        Task<IEnumerable<IExchangeEventEntity>> GetForLastDayByExchangeAddressAsync(string address);
        Task<IEnumerable<IExchangeEventEntity>> GetSortedByDateRangeAsync(string exchangeAddress, DateTime start, DateTime end, int limit);
        Task<IEnumerable<IExchangeEventEntity>> FindByAsync(string userAddress, string exchangeAddress);

        Task<IEnumerable<IExchangeChartAggregationResultElement>> GetChartsAsync(
            string exchangeAddress, DateTime start, DateTime end, int chartIntervalUnit);
    }
}