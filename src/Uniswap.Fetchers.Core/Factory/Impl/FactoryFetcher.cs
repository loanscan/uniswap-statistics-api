using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Uniswap.Fetchers.Core.Eth;
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.Fetchers.Core.Factory.Impl
{
    public class FactoryFetcher : IFetcher
    {
        private readonly IStartBlockProvider _startBlockProvider;
        private readonly IRecentBlockProvider _recentBlockProvider;
        private readonly IEventsFetcher<NewExchangeEventDTO> _newExchangeEventsFetcher;
        private readonly IExchangeProcessor _exchangeExchangeProcessor;
        private readonly FetcherSettings _settings;
        private readonly ILogger<FactoryFetcher> _logger;

        public FactoryFetcher(
            IStartBlockProvider startBlockProvider,
            IRecentBlockProvider recentBlockProvider,
            IEventsFetcher<NewExchangeEventDTO> newExchangeEventsFetcher,
            IExchangeProcessor exchangeExchangeProcessor,
            FetcherSettings settings,
            ILogger<FactoryFetcher> logger)
        {
            _startBlockProvider = startBlockProvider;
            _recentBlockProvider = recentBlockProvider;
            _newExchangeEventsFetcher = newExchangeEventsFetcher;
            _exchangeExchangeProcessor = exchangeExchangeProcessor;
            _settings = settings;
            _logger = logger;
        }

        public async Task FetchAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching...");

            var startBlock = await _startBlockProvider.GetAsync();

            _logger.LogInformation("startBlock is {startBlock}", startBlock);

            var range = await GetRange(startBlock);

            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching events from {fromBlock} to {toBlock}...", range.FromBlock,
                    range.ToBlock);

                var eventLogs = await _newExchangeEventsFetcher.FetchAsync(range.FromBlock, range.ToBlock);

                _logger.LogInformation("{amount} event(s) fetched", eventLogs.Count);

                foreach (var eventLog in eventLogs)
                {
                    await _exchangeExchangeProcessor.ProcessAsync(eventLog);
                }

                await Task.Delay(range.IsFull ? 0 : _settings.DelayMs, cancellationToken);

                range = await GetRange(range.ToBlock + 1);
            }
        }

        private async Task<(ulong FromBlock, ulong ToBlock, bool IsFull)> GetRange(ulong startBlock)
        {
            var recentBlock = await _recentBlockProvider.GetAsync();

            var fromBlock = Math.Min(startBlock, recentBlock - _settings.RecentBlockReachLimit);
            var toBlock = Math.Min(startBlock + _settings.BlocksPerIteration,
                recentBlock - _settings.RecentBlockReachLimit);

            return (fromBlock, toBlock, toBlock - fromBlock == _settings.BlocksPerIteration);
        }
    }
}