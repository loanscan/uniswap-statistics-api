using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.Fetchers.Core.Utils;

namespace Uniswap.Fetchers.Core.Token.Impl.Ethplorer
{
    public class EthplorerTokenInfoProvider : ITokenInfoProvider
    {
        private readonly IRetrier _retrier;
        private readonly EthplorerApi _api;

        public EthplorerTokenInfoProvider(
            IRetrier retrier,
            EthplorerApi api)
        {
            _retrier = retrier;
            _api = api;
        }

        public async Task<TokenInfo> GetAsync(string address)
        {
            var response = await _retrier.ExecuteAsync(() => _api.GetTokenInfoAsync(address));

            return new TokenInfo
            {
                Name = response.Name.IsNullOrEmpty() ? Constants.DefaultName : response.Name,
                Symbol = response.Symbol.IsNullOrEmpty() ? Constants.DefaultSymbol : response.Symbol,
                Decimals = response.Name.IsNullOrEmpty() ? Constants.DefaultDecimals : response.Decimals
            };
        }
    }
}