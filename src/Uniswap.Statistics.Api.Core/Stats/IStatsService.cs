using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.Stats
{
    public interface IStatsService
    {
        Task<IEnumerable<IExchangeEntity>> GetExchangesAsync(StatsOrderBy orderBy, decimal minEthLiquidityAmount = 0);
    }
}