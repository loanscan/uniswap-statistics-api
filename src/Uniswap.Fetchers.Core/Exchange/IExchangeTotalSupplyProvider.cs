using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeTotalSupplyProvider
    {
        Task<decimal> GetAsync(string exchangeAddress);
    }
}