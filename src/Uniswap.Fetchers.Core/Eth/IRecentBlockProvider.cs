using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Eth
{
    public interface IRecentBlockProvider
    {
        Task<ulong> GetAsync();
    }
}