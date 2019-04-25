using System.Threading.Tasks;
using Uniswap.Data.Repositories;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class StartBlockService : IStartBlockService
    {
        private readonly ILastBlockFetchedByExchangeRepository _repository;
        private readonly ILastBlockFetchedByExchangeEntityFactory _entityFactory;

        public StartBlockService(
            ILastBlockFetchedByExchangeRepository repository,
            ILastBlockFetchedByExchangeEntityFactory entityFactory)
        {
            _repository = repository;
            _entityFactory = entityFactory;
        }
        
        public async Task<ulong> GetAsync(string exchangeAddress, ulong genesisBlockNumber)
        {
            var entity = await _repository.FindByIdAsync(exchangeAddress);

            return entity?.LastBlock ?? genesisBlockNumber;
        }

        public async Task UpdateAsync(string exchangeAddress, ulong blockNumber)
        {
            var entity = _entityFactory.Create(exchangeAddress, blockNumber);

            await _repository.AddOrUpdateAsync(entity);
        }
    }
}