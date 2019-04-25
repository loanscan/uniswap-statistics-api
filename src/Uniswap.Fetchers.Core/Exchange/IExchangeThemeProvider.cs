using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IExchangeThemeProvider
    {
        Task<string> GetAsync(string symbol);
    }
}