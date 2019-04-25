using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Token
{
    public interface IDSErc20Gateway
    {
        Task<byte[]> GetNameAsync();
        Task<byte[]> GetSymbolAsync();
    }
}