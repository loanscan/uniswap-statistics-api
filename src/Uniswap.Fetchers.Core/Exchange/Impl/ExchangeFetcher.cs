using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Uniswap.Data.Repositories;
using Uniswap.Fetchers.Core.Eth;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeFetcher : IFetcher
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IStartBlockService _startBlockService;
        private readonly IRecentBlockProvider _recentBlockProvider;
        private readonly ExchangeFetcherSettings _fetcherSettings;
        private readonly Func<string, ExchangeEventsFetcher> _exchangeEventsFetcherFactory;
        private readonly IExchangeEventsProcessor _exchangeEventsProcessor;
        private readonly ILogger<ExchangeFetcher> _logger;

        public ExchangeFetcher(
            IExchangeRepository exchangeRepository,
            IStartBlockService startBlockService,
            IRecentBlockProvider recentBlockProvider,
            ExchangeFetcherSettings fetcherSettings,
            Func<string, ExchangeEventsFetcher> exchangeEventsFetcherFactory,
            IExchangeEventsProcessor exchangeEventsProcessor,
            ILogger<ExchangeFetcher> logger)
        {
            _exchangeRepository = exchangeRepository;
            _startBlockService = startBlockService;
            _recentBlockProvider = recentBlockProvider;
            _fetcherSettings = fetcherSettings;
            _exchangeEventsFetcherFactory = exchangeEventsFetcherFactory;
            _exchangeEventsProcessor = exchangeEventsProcessor;
            _logger = logger;
        }

        public async Task FetchAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching...");

            while (!cancellationToken.IsCancellationRequested)
            {
                var startDate = DateTime.UtcNow;
                
                var exchanges = await _exchangeRepository.GetAllAsync();
                
                _logger.LogInformation("There are {amount} exchange(s)", exchanges.Count());

                while (!cancellationToken.IsCancellationRequested)
                {                    
                    foreach (var exchange in exchanges)
                    {
                        var fromBlock =
                            await _startBlockService.GetAsync(exchange.Id, exchange.BlockNumber);
                        var recentBlock = await _recentBlockProvider.GetAsync() - _fetcherSettings.RecentBlockReachLimit;

                        var fetcher = _exchangeEventsFetcherFactory(exchange.Id);

                        _logger.LogInformation("Fetching events for exchange {exchangeAddress}...", exchange.Id);

                        while (fromBlock <= recentBlock)
                        {
                            var toBlock = Math.Min(fromBlock + _fetcherSettings.BlocksPerIteration, recentBlock);
                            
                            _logger.LogInformation("Fetching events from {fromBlock} to {toBlock}...", fromBlock, toBlock);

                            var events = await fetcher.FetchAsync(fromBlock, toBlock);
                            
                            _logger.LogInformation("{amount} event(s) fetched", events.Count);

                            await _exchangeEventsProcessor.ProcessAsync(events);

                            await _startBlockService.UpdateAsync(exchange.Id, toBlock);

                            fromBlock = toBlock + 1;
                        }
                    }

                    await Task.Delay(_fetcherSettings.DelayMs, cancellationToken);

                    if (DateTime.UtcNow - startDate > TimeSpan.FromSeconds(_fetcherSettings.UpdateExchangesIntervalMs))
                    {
                        break;
                    }
                }
            }
        }
    }
}