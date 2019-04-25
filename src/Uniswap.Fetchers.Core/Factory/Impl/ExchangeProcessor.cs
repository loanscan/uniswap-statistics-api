using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nethereum.Contracts;
using Uniswap.Data.Repositories;
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.Fetchers.Core.Factory.Impl
{
    public class ExchangeProcessor : IExchangeProcessor
    {
        private readonly IExchangeMapper _mapper;
        private readonly IExchangeRepository _repository;
        private readonly ILogger<ExchangeProcessor> _logger;

        public ExchangeProcessor(
            IExchangeMapper mapper,
            IExchangeRepository repository,
            ILogger<ExchangeProcessor> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task ProcessAsync(EventLog<NewExchangeEventDTO> eventLog)
        {
            var entity = await _mapper.MapAsync(eventLog);

            await _repository.AddOrUpdate(entity);

            _logger.LogInformation("Event txHash {txHash} logIndex {logIndex} was processed",
                eventLog.Log.TransactionHash, eventLog.Log.LogIndex.Value);
        }
    }
}