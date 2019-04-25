using System.Threading.Tasks;
using Uniswap.Common;

namespace Uniswap.Statistics.Api.Core.Ticker
{
    public interface ITickerService
    {
        Task<OperationResult<ExchangeTicker>> GetByAddress(string exchangeAddress);
    }
}