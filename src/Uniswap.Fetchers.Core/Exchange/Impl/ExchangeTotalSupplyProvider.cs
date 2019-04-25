using System;
using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Token;
using Uniswap.Fetchers.Core.Utils;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeTotalSupplyProvider : IExchangeTotalSupplyProvider
    {
        private readonly Func<string, IExchangeGateway> _exchangeGatewayFactory;
        private readonly ITokenInfoProvider _tokenInfoProvider;

        public ExchangeTotalSupplyProvider(
            Func<string, IExchangeGateway> exchangeGatewayFactory,
            ITokenInfoProvider tokenInfoProvider)
        {
            _exchangeGatewayFactory = exchangeGatewayFactory;
            _tokenInfoProvider = tokenInfoProvider;
        }
        
        public async Task<decimal> GetAsync(string exchangeAddress)
        {
            var exchangeGateway = _exchangeGatewayFactory(exchangeAddress);
            
            var totalSupply = await exchangeGateway.GetTotalSupplyAsync();
            var tokenInfo = await _tokenInfoProvider.GetAsync(exchangeAddress);

            return totalSupply.ToDecimal(tokenInfo.Decimals);
        }
    }
}