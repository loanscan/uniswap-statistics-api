using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.User.Dtos
{
    public class UserDto
    {
        [JsonProperty("poolTotalSupply")]
        public string PoolTotalSupply { get; set; }
        
        [JsonProperty("userNumPoolTokens")]
        public string UserNumPoolTokens { get; set; }
        
        [JsonProperty("userPoolPercent")]
        public string UserPoolPercent { get; set; }
        
        [JsonProperty("userEthLiquidity")]
        public string UserEthLiquidity { get; set; }
        
        [JsonProperty("ethFees")]
        public decimal EthFees { get; set; }
        
        [JsonProperty("userTokenLiquidity")]
        public string UserTokenLiquidity { get; set; }
        
        [JsonProperty("tokenFees")]
        public decimal TokenFees { get; set; }
    }
}