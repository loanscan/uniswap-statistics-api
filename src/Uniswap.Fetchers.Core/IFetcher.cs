using System.Threading;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core
{
    public interface IFetcher
    {
        Task FetchAsync(CancellationToken cancellationToken);
    }
}