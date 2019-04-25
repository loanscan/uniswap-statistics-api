using System;
using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.SmartContracts.DSErc20.Service;

namespace Uniswap.Fetchers.Core.Token.Impl
{
    public class DSErc20Gateway : IDSErc20Gateway
    {
        private readonly IRetrier _retrier;
        private readonly DSErc20Service _dsErc20Service;

        public DSErc20Gateway(
            IRetrier retrier,
            string address,
            Func<string, DSErc20Service> dsErc20ServiceFactory)
        {
            _retrier = retrier;
            _dsErc20Service = dsErc20ServiceFactory(address);

        }
        
        public async Task<byte[]> GetNameAsync()
        {
            return await _retrier.ExecuteAsync(() => _dsErc20Service.NameQueryAsync());
        }

        public async Task<byte[]> GetSymbolAsync()
        {
            return await _retrier.ExecuteAsync(() => _dsErc20Service.SymbolQueryAsync());
        }
    }
}