using System;
using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Utils;

namespace Uniswap.Fetchers.Core.Token.Impl
{
    public class FromErc20TokenInfoProvider : ITokenInfoProvider
    {
        private readonly Func<string, IErc20Gateway> _erc20GatewayFactory;
        private readonly Func<string, IDSErc20Gateway> _dsErc20GatewayFactory;

        public FromErc20TokenInfoProvider(
            Func<string, IErc20Gateway> erc20GatewayFactory,
            Func<string, IDSErc20Gateway> dsErc20GatewayFactory)
        {
            _erc20GatewayFactory = erc20GatewayFactory;
            _dsErc20GatewayFactory = dsErc20GatewayFactory;
        }
        
        public async Task<TokenInfo> GetAsync(string address)
        {
            var erc20Gateway = _erc20GatewayFactory(address);
            var dsErc20Gateway = _dsErc20GatewayFactory(address);

            string name;
            try
            {
                name = await erc20Gateway.GetNameAsync();
            }
            // We can get OverflowException with message "Value was either too large or too small for an Int32"
            // We can get ArgumentOutOfRangeException with message "Index and count must refer to a location within the buffer"
            // In such situation we will try to call DSTokenService because name can be stored as bytes32
            // Same logic is applied to GetSymbolAsync()
            catch (Exception ex) when (ex is OverflowException || ex is ArgumentOutOfRangeException)
            {
                name = (await dsErc20Gateway.GetNameAsync()).ToStringWithTrimming();
            }

            string symbol;
            try
            {
                symbol = await erc20Gateway.GetSymbolAsync();
            }
            catch (Exception ex) when (ex is OverflowException || ex is ArgumentOutOfRangeException)
            {
                symbol = (await dsErc20Gateway.GetSymbolAsync()).ToStringWithTrimming();
            }

            var decimals = await erc20Gateway.GetDecimalsAsync();

            return new TokenInfo
            {
                Name = name.IsNullOrEmpty() ? Constants.DefaultName : name,
                Symbol = symbol.IsNullOrEmpty() ? Constants.DefaultSymbol : symbol,
                Decimals = name.IsNullOrEmpty() ? Constants.DefaultDecimals : (int) decimals
            };
        }
    }
}