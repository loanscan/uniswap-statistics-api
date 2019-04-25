using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Token
{
    public interface ITokenLiquidityProvider
    {
        Task<decimal> GetAsync(string tokenAddress, string ownerAddress);
        Task<decimal> GetAsync(string tokenAddress, string ownerAddress, ulong blockNumber);
    }
}