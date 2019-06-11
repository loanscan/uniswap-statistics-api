using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Core.Stats;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Stats.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Stats.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class StatsController : ApiControllerBase
    {
        private readonly IStatsService _statsService;
        private readonly IMapper _mapper;

        public StatsController(
            IStatsService statsService,
            IMapper mapper)
        {
            _statsService = statsService;
            _mapper = mapper;
        }

        /// <summary>
        /// System-wide stats for all exchanges available on Uniswap.
        /// </summary>
        /// <returns>System-wide stats for all exchanges</returns>
        [HttpGet]
        [ProducesResponseType(typeof(StatsExchangesDto), 200)]
        public async Task<IActionResult> GetStats([FromQuery] StatsOrderBy orderBy = StatsOrderBy.Liquidity)
        {
            var exchanges = await _statsService.GetExchangesAsync(orderBy);

            return Ok(_mapper.Map<List<IExchangeEntity>, StatsExchangesDto>(exchanges.ToList()));
        }
    }
}