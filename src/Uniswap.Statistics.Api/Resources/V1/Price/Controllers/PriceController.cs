using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Core.Price;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Price.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Price.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class PriceController : ApiControllerBase
    {
        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;

        public PriceController(
            IPriceService priceService,
            IMapper mapper)
        {
            _priceService = priceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns current symbol price.
        /// </summary>
        /// <returns>Current symbol price.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PriceDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPriceAsync([FromQuery, Required] string symbol)
        {            
            var exchange = await _priceService.GetExchangeAsync(symbol);

            if (exchange == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<IExchangeEntity, PriceDto>(exchange));
            }
        }
    }
}