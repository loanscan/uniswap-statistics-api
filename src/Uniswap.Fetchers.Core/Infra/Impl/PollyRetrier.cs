using System;
using System.Threading.Tasks;
using Polly;

namespace Uniswap.Fetchers.Core.Infra.Impl
{
    public class PollyRetrier : IRetrier
    {
        private const int RetryCount = 3;
        
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> funcOfTask)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            return await retryPolicy.ExecuteAsync(funcOfTask);
        }
    }
}