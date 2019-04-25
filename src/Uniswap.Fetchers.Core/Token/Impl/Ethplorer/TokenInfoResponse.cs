using Newtonsoft.Json;

namespace Uniswap.Fetchers.Core.Token.Impl.Ethplorer
{
    public class TokenInfoResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("decimals")]
        public int Decimals { get; set; }
    }
}