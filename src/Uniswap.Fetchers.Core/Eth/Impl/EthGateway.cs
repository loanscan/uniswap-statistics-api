using System.Numerics;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Uniswap.Fetchers.Core.Infra;

namespace Uniswap.Fetchers.Core.Eth.Impl
{
    public class EthGateway : IEthGateway
    {
        private readonly IRetrier _retrier;
        private readonly Web3 _web3;

        public EthGateway(
            IRetrier retrier,
            Web3 web3)
        {
            _retrier = retrier;
            _web3 = web3;
        }

        public async Task<BigInteger> GetRecentBlockAsync()
        {
            return await _retrier.ExecuteAsync(() => _web3.Eth.Blocks.GetBlockNumber.SendRequestAsync());
        }

        public async Task<BigInteger> GetBalanceAsync(string address)
        {
            return await _retrier.ExecuteAsync(() => _web3.Eth.GetBalance.SendRequestAsync(address));
        }

        public async Task<BigInteger> GetBalanceAsync(string address, ulong blockNumber)
        {
            return await _retrier.ExecuteAsync(() => _web3.Eth.GetBalance.SendRequestAsync(address, new BlockParameter(blockNumber)));
        }

        public async Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesAsync(ulong blockNumber)
        {
            return await _retrier.ExecuteAsync(() => _web3.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(new BlockParameter(blockNumber)));
        }
    }
}