using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Exchange
{
    public interface IStartBlockService
    {
        Task<ulong> GetAsync(string exchangeAddress, ulong genesisBlockNumber);
        Task UpdateAsync(string exchangeAddress, ulong blockNumber);
    }
}