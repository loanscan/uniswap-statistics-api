using System.Threading.Tasks;

namespace Uniswap.Statistics.Api.Core.User
{
    public interface IUserService
    {
        Task<UserInfo> GetByExchangeAddress(string exchangeAddress, string userAddress);
    }
}