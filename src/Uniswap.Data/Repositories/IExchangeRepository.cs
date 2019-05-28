using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Repositories
{
    public interface IExchangeRepository
    {
        Task AddOrUpdate(IExchangeEntity entity);
        Task<IExchangeEntity> FindByAsync(string symbol = null, string tokenAddress = null, string exchangeAddress = null);
        Task<IEnumerable<IExchangeEntity>> GetAllAsync();
        Task<IExchangeEntity> GetLastCreatedAsync();
        Task Update(string exchangeId, decimal ethLiquidity, decimal tokenLiquidity, decimal totalSupply);
        Task<IEnumerable<IExchangeEntity>> GetAsync(decimal minEthLiquidityAmount);
    }
}