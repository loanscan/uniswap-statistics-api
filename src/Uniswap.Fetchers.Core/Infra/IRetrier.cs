using System;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Infra
{
    public interface IRetrier
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> funcOfTask);
    }
}