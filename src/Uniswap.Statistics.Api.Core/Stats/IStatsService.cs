using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.Stats
{
    public interface IStatsService
    {
        Task<IEnumerable<IExchangeEntity>> GetAllExchangesAsync(StatsOrderBy? orderBy);
    }
}