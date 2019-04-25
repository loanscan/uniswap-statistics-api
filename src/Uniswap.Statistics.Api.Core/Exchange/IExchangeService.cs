using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.Exchange
{
    public interface IExchangeService
    {
        Task<IExchangeEntity> GetExchangeAsync(string symbol = null, string tokenAddress = null,
            string exchangeAddress = null);
    }
}