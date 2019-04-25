using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Uniswap.Statistics.Api.Core.User;
using Uniswap.Statistics.Api.Resources.Base;
using Uniswap.Statistics.Api.Resources.V1.User.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.User.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Returns info regarding a specific user in this exchange.
        /// </summary>
        /// <returns>Info regarding a specific user in this exchange.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), 200)]    
        public async Task<IActionResult> GetUserInfo([FromQuery, Required] string exchangeAddress, [FromQuery, Required] string userAddress)
        {
            var userInfo = await _userService.GetByExchangeAddress(exchangeAddress, userAddress);

            return Ok(_mapper.Map<UserInfo, UserDto>(userInfo));
        }
    }
}