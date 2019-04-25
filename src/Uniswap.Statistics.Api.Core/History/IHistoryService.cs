using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uniswap.Statistics.Api.Core.History
{
    public interface IHistoryService
    {
        Task<IEnumerable<ExchangeEvent>> GetByExchangeAddress(string address, long startTime, long endTime, int count);
    }
}