using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Statistics.Api.Core.Ticker;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Ticker.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Ticker.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TickerController : ApiControllerBase
    {
        private readonly ITickerService _tickerService;
        private readonly IMapper _mapper;

        public TickerController(
            ITickerService tickerService,
            IMapper mapper)
        {
            _tickerService = tickerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns symbol data for the previous 24 hours.
        /// </summary>
        /// <returns>Symbol data for the previous 24 hours.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(TickerDto), 200)]
        public async Task<IActionResult> GetTicker([FromQuery, Required] string exchangeAddress)
        {
            var result = await _tickerService.GetByAddress(exchangeAddress);
            if (!result.IsCompleted) return NotFound();
            return Ok(result.Result == null ? new object() : _mapper.Map<ExchangeTicker, TickerDto>(result.Result));
        }
    }
}