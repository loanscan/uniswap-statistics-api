using System.Threading.Tasks;
using Uniswap.Data.Repositories;

namespace Uniswap.Fetchers.Core.Factory.Impl
{
    public class StartBlockProvider : IStartBlockProvider
    {
        private const ulong GenesisBlock = 6627917;
        
        private readonly IExchangeRepository _repository;

        public StartBlockProvider(IExchangeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ulong> GetAsync()
        {
            var lastCreatedExchange = await _repository.GetLastCreatedAsync();
            return lastCreatedExchange?.BlockNumber ?? GenesisBlock;
        }
    }
}