using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Statistics.Api.Core.Chart;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Chart.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Chart.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ChartController : ApiControllerBase
    {
        private readonly IChartService _chartService;
        private readonly IMapper _mapper;

        public ChartController(
            IChartService chartService,
            IMapper mapper)
        {
            _chartService = chartService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns data for charts.
        /// </summary>
        /// <returns>Symbol data for the previous 24 hours.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ChartDto>), 200)]
        public async Task<IActionResult> GetChart(
            [FromQuery, Required] string exchangeAddress,
            [FromQuery, Required] long startTime,
            [FromQuery, Required] long endTime,
            [FromQuery, Required] ChartUnit unit)
        {
            var charts = await _chartService.GetChartData(exchangeAddress, startTime, endTime, unit);
            
            return Ok(_mapper.Map<List<Core.Chart.Chart>, List<ChartDto>>(charts.ToList()));
        }
    }
}