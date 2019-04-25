using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Uniswap.Fetchers.Core;

namespace Uniswap.Fetchers.HostedServices
{
    public class ExchangesHostedService : BackgroundService
    {
        private readonly IFetcher _exchangeFetcher;
        private readonly ILogger<ExchangesHostedService> _logger;
       
        public ExchangesHostedService(
            IFetcher exchangeFetcher,
            ILogger<ExchangesHostedService> logger)
        {
            _exchangeFetcher = exchangeFetcher;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Fetching...");
                    
                    await _exchangeFetcher.FetchAsync(stoppingToken);
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