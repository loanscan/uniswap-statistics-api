using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Core.Stats;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Directory.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Directory.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class DirectoryController : ApiControllerBase
    {
        private readonly IStatsService _statsService;
        private readonly IMapper _mapper;

        public DirectoryController(
            IStatsService statsService,
            IMapper mapper)
        {
            _statsService = statsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all exchanges available on Uniswap.
        /// </summary>
        /// <returns>All exchanges available on Uniswap.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DirectoriesDto), 200)]
        public async Task<IActionResult> GetAllDirectoriesAsync(
            [FromQuery] decimal minEthLiquidity = 0,
            [FromQuery] StatsOrderBy orderBy = StatsOrderBy.Liquidity
        )
        {
            var directories = await _statsService.GetExchangesAsync(orderBy, minEthLiquidity);

            return Ok(_mapper.Map<List<IExchangeEntity>, DirectoriesDto>(directories.ToList()));
        }
    }
}