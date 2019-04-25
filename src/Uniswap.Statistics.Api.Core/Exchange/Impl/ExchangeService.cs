using System.Threading.Tasks;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Exchange.Impl
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository _repository;

        public ExchangeService(IExchangeRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IExchangeEntity> GetExchangeAsync(string symbol = null, string tokenAddress = null, string exchangeAddress = null)
        {
            return await _repository.FindByAsync(symbol, tokenAddress, exchangeAddress);
        }
    }
}