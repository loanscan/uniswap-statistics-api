using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Factory
{
    public interface IStartBlockProvider
    {
        Task<ulong> GetAsync();
    }
}