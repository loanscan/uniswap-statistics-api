using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Uniswap.Fetchers.Core.Token.Impl.Ethplorer
{
    public class EthplorerApi : IDisposable
    {
        private readonly EthplorerSettings _settings;

        private readonly HttpClient _httpClient;

        public EthplorerApi(
            EthplorerSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.Url)
            };
        }
        
        public async Task<TokenInfoResponse> GetTokenInfoAsync(string address)
        {
            var message = await _httpClient.GetAsync(new Uri($"/getTokenInfo/{address}?apiKey={_settings.ApiKey}", UriKind.Relative));
            var responseAsString = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenInfoResponse>(responseAsString);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}