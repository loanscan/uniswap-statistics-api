using System.Collections.Generic;
using System.Threading.Tasks;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Directory.Impl
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IExchangeRepository _repository;

        public DirectoryService(IExchangeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<IExchangeEntity>> GetDirectoriesAsync(decimal minEthLiquidityAmount)
        {
            return await _repository.GetAsync(minEthLiquidityAmount);
        }
    }
}