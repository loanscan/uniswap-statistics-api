using System.Numerics;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;

namespace Uniswap.Fetchers.Core.Eth
{
    public interface IEthGateway
    {
        Task<BigInteger> GetRecentBlockAsync();
        Task<BigInteger> GetBalanceAsync(string address);
        Task<BigInteger> GetBalanceAsync(string address, ulong blockNumber);
        Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesAsync(ulong blockNumber);
    }
}