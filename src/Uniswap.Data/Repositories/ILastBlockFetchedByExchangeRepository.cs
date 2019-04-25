using System.Threading.Tasks;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Repositories
{
    public interface ILastBlockFetchedByExchangeRepository
    {
        Task<ILastBlockFetchedByExchangeEntity> FindByIdAsync(string id);
        Task AddOrUpdateAsync(ILastBlockFetchedByExchangeEntity entity);
    }
}