using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Core.Directory;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.Directory.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Directory.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class DirectoryController : ApiControllerBase
    {
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;

        public DirectoryController(
            IDirectoryService directoryService,
            IMapper mapper)
        {
            _directoryService = directoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all exchanges available on Uniswap.
        /// </summary>
        /// <returns>All exchanges available on Uniswap.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DirectoriesDto), 200)]
        public async Task<IActionResult> GetAllDirectoriesAsync([FromQuery] decimal minEthLiquidity = 0)
        {
            var directories = await _directoryService.GetDirectoriesAsync(minEthLiquidity);

            return Ok(_mapper.Map<List<IExchangeEntity>, DirectoriesDto>(directories.ToList()));
        }
    }
}