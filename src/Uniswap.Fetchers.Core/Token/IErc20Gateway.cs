using System.Numerics;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Token
{
    public interface IErc20Gateway
    {
        Task<string> GetNameAsync();
        Task<string> GetSymbolAsync();
        Task<BigInteger> GetDecimalsAsync();
        Task<BigInteger> GetBalanceAsync(string address);
        Task<BigInteger> GetBalanceAsync(string address, ulong blockNumber);
    }
}