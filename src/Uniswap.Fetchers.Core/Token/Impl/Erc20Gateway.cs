using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.SmartContracts.Erc20.Service;

namespace Uniswap.Fetchers.Core.Token.Impl
{
    public class Erc20Gateway : IErc20Gateway
    {
        private readonly IRetrier _retrier;
        private readonly Erc20Service _erc20Service;

        public Erc20Gateway(
            IRetrier retrier,
            string address,
            Func<string, Erc20Service> erc20ServiceFactory)
        {
            _retrier = retrier;
            _erc20Service = erc20ServiceFactory(address);
        }

        public async Task<string> GetNameAsync()
        {
            return await _retrier.ExecuteAsync(() => _erc20Service.NameQueryAsync());
        }

        public async Task<string> GetSymbolAsync()
        {
            return await _retrier.ExecuteAsync(() => _erc20Service.SymbolQueryAsync());
        }

        public async Task<BigInteger> GetDecimalsAsync()
        {
            return await _retrier.ExecuteAsync(() => _erc20Service.DecimalsQueryAsync());
        }

        public async Task<BigInteger> GetBalanceAsync(string address)
        {
            return await _retrier.ExecuteAsync(() => _erc20Service.BalanceOfQueryAsync(address));
        }
        
        public async Task<BigInteger> GetBalanceAsync(string address, ulong blockNumber)
        {
            return await _retrier.ExecuteAsync(() => _erc20Service.BalanceOfQueryAsync(address, new BlockParameter(blockNumber)));
        }
    }
}