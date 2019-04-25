using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Core.Exchange;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Exchange.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Exchange.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ExchangeController : ApiControllerBase
    {
        private readonly IExchangeService _exchangeService;
        private readonly IMapper _mapper;

        public ExchangeController(
            IExchangeService exchangeService,
            IMapper mapper)
        {
            _exchangeService = exchangeService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Returns exchange details.
        /// </summary>
        /// <returns>Exchange details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ExchangeDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetExchangeAsync([FromQuery] string symbol = null, [FromQuery] string tokenAddress = null, [FromQuery] string exchangeAddress = null)
        {
            var exchange = await _exchangeService.GetExchangeAsync(symbol, tokenAddress, exchangeAddress);

            if (exchange == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<IExchangeEntity, ExchangeDto>(exchange));
            }
        }
    }
}