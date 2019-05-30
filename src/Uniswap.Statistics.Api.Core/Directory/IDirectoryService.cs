using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.Directory
{
    public interface IDirectoryService
    {
        Task<IEnumerable<IExchangeEntity>> GetDirectoriesAsync(decimal minEthLiquidityAmount);
    }
}