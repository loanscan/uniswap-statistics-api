using System.Threading.Tasks;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.Price.Impl
{
    public class PriceService : IPriceService
    {
        private readonly IExchangeRepository _exchangeRepository;

        public PriceService(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }
        
        public async Task<IExchangeEntity> GetExchangeAsync(string symbol)
        {
            return await _exchangeRepository.FindByAsync(symbol);
        }
    }
}