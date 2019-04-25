using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Token
{
    public interface ITokenInfoProvider
    {
        Task<TokenInfo> GetAsync(string address);
    }
}