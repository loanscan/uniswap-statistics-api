using System;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Eth
{
    public interface IBlockTimestampProvider
    {
        Task<DateTime> GetByBlockNumberAsync(ulong blockNumber);
    }
}