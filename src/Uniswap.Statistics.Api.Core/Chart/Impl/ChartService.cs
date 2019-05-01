using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniswap.Common;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Chart.Impl
{
    public class ChartService : IChartService
    {     
        private readonly IExchangeEventsRepository _exchangeEventsRepository;

        public ChartService(IExchangeEventsRepository exchangeEventsRepository)
        {
            _exchangeEventsRepository = exchangeEventsRepository;
        }

        public async Task<IEnumerable<Chart>> GetChartData(string exchangeAddress, long startTime, long endTime, ChartUnit unit)
        {
            
            var start = startTime.ToUnixUtcDateTime();
            var end = endTime.ToUnixUtcDateTime();
            var chartAggregation = await _exchangeEventsRepository.BuildChartAggregation(exchangeAddress, start, end, (int)unit);
            
            return chartAggregation.Select(chart => new Chart
            {
                Date = new DateTime(chart.Id.Year, chart.Id.Month, chart.Id.Day),
                EthLiquidity = chart.EthLiquidity,
                EthVolume = chart.EthVolume,
                TokenLiquidity = chart.TokenLiquidity,
                MarginalEthRate = chart.EthLiquidity == 0 ? 0 : chart.TokenLiquidity / chart.EthLiquidity
            });
        }
    }
}