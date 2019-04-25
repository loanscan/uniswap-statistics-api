using System;
using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Utils;

namespace Uniswap.Fetchers.Core.Token.Impl
{
    public class FromErc20TokenLiquidityProvider : ITokenLiquidityProvider
    {
        private readonly ITokenInfoProvider _tokenInfoProvider;
        private readonly Func<string, IErc20Gateway> _erc20GatewayFactory;

        public FromErc20TokenLiquidityProvider(
            ITokenInfoProvider tokenInfoProvider,
            Func<string, IErc20Gateway> erc20GatewayFactory)
        {
            _tokenInfoProvider = tokenInfoProvider;
            _erc20GatewayFactory = erc20GatewayFactory;
        }

        public async Task<decimal> GetAsync(string tokenAddress, string ownerAddress)
        {
            var tokenInfo = await _tokenInfoProvider.GetAsync(tokenAddress);
            var balance = await _erc20GatewayFactory(tokenAddress).GetBalanceAsync(ownerAddress);

            return balance.ToDecimal(tokenInfo.Decimals);
        }

        public async Task<decimal> GetAsync(string tokenAddress, string ownerAddress, ulong blockNumber)
        {
            var tokenInfo = await _tokenInfoProvider.GetAsync(tokenAddress);
            var balance = await _erc20GatewayFactory(tokenAddress).GetBalanceAsync(ownerAddress, blockNumber);

            return balance.ToDecimal(tokenInfo.Decimals);
        }
    }
}