using System.Linq;
using System.Threading.Tasks;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.User.Impl
{
    public class UserService : IUserService
    {
        private readonly IExchangeEventsRepository _exchangeEventsRepository;
        private readonly IExchangeRepository _exchangeRepository;

        public UserService(IExchangeEventsRepository exchangeEventsRepository, IExchangeRepository exchangeRepository)
        {
            _exchangeEventsRepository = exchangeEventsRepository;
            _exchangeRepository = exchangeRepository;
        }

        public async Task<UserInfo> GetByExchangeAddress(string exchangeAddress, string userAddress)
        {
            var exchangeEntity = await _exchangeRepository.FindByAsync(exchangeAddress: exchangeAddress);
            var userEvents = await _exchangeEventsRepository.FindByAsync(userAddress, exchangeAddress);
            var poolTotalSupply = exchangeEntity.TotalSupply;
            
            var userPoolTokens = userEvents.OrderBy(x => x.BlockNumber).ThenBy(x => x.LogIndex).LastOrDefault()?.CallerBalance ?? 0;
            
            return new UserInfo
            {
                EthFees = userEvents.Sum(x => x.EthFee),
                PoolTotalSupply = poolTotalSupply,
                TokenFees = userEvents.Sum(x => x.TokenFee),
                UserNumPoolTokens = userPoolTokens,
                UserPoolPercent = poolTotalSupply == 0 ? 0 : userPoolTokens / poolTotalSupply
            };
        }
    }
}