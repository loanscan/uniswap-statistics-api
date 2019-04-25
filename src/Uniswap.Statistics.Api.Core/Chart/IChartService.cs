using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uniswap.Statistics.Api.Core.Chart
{
    public interface IChartService
    {
        Task<IEnumerable<Chart>> GetChartData(
            string exchangeAddress,
            long startTime,
            long endTime,
            ChartUnit unit);
    }
}