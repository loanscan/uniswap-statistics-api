using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Statistics.Api.Core.History;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.History.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.History.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class HistoryController : ApiControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public HistoryController(
            IHistoryService historyService,
            IMapper mapper)
        {
            _historyService = historyService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns trading history within a given timespan.
        /// </summary>
        /// <returns>Trading history within a given timespan.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EventDto>), 200)]
        public async Task<IActionResult> GetHistory(
            [FromQuery, Required] string exchangeAddress,
            [FromQuery, Required] long startTime,
            [FromQuery, Required] long endTime,
            [FromQuery] int count = 50)
        {
            var exchangeEvents = await _historyService.GetByExchangeAddress(exchangeAddress, startTime, endTime, count);
            
            return Ok(_mapper.Map<List<ExchangeEvent>, List<EventDto>>(exchangeEvents.ToList()));
        }
    }
}