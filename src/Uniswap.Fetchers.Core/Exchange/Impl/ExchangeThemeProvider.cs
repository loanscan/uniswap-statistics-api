using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeThemeProvider : IExchangeThemeProvider
    {
        private readonly Dictionary<string, string> _themes;

        public ExchangeThemeProvider(Dictionary<string, string> themes)
        {
            _themes = themes;
        }

        public Task<string> GetAsync(string symbol)
        {
            if (!_themes.TryGetValue(symbol, out var theme))
            {
                theme = string.Empty;
            }

            return Task.FromResult(theme);
        }
    }
}