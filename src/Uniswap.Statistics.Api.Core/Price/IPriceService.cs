using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.Price
{
    public interface IPriceService
    {
        Task<IExchangeEntity> GetExchangeAsync(string symbol);
    }
}