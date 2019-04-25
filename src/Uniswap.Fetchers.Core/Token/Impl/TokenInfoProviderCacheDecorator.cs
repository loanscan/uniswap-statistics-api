using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Token.Impl
{
    public class TokenInfoProviderCacheDecorator : ITokenInfoProvider
    {
        private readonly ITokenInfoProvider _tokenInfoProvider;

        private readonly Dictionary<string, TokenInfo> _cache;

        public TokenInfoProviderCacheDecorator(ITokenInfoProvider tokenInfoProvider)
        {
            _tokenInfoProvider = tokenInfoProvider;
            
            _cache = new Dictionary<string, TokenInfo>();
        }
        
        public async Task<TokenInfo> GetAsync(string address)
        {
            if (_cache.TryGetValue(address, out var cachedTokenInfo))
            {
                return cachedTokenInfo;
            }
            else
            {
                var tokenInfo = await _tokenInfoProvider.GetAsync(address);
                
                _cache[address] = tokenInfo;

                return tokenInfo;
            }
        }
    }
}