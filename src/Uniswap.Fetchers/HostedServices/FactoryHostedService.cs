using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Uniswap.Fetchers.Core;

namespace Uniswap.Fetchers.HostedServices
{
    public class FactoryHostedService : BackgroundService
    {
        private readonly IFetcher _fetcher;
        private readonly ILogger<FactoryHostedService> _logger;

        public FactoryHostedService(
            IFetcher fetcher,
            ILogger<FactoryHostedService> logger)
        {
            _fetcher = fetcher;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Fetching...");
                    
                    await _fetcher.FetchAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Execution is canceled");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unknown error occurred");
                }
            }
        }
    }
}