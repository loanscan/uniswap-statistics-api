using System.Numerics;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeGateway
    {
        Task<string> GetTokenAddressAsync();
        Task<BigInteger> GetBalanceOfAsync(string address, ulong blockNumber);
        Task<BigInteger> GetTotalSupplyAsync();
    }
}