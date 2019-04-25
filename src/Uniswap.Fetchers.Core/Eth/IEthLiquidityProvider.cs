using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Eth
{
    public interface IEthLiquidityProvider
    {
        Task<decimal> GetAsync(string address);
        Task<decimal> GetAsync(string address, ulong blockNumber);
    }
}