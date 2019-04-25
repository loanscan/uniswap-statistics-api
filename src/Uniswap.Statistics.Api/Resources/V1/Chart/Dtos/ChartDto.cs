using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Chart.Dtos
{
    public class ChartDto
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        
        [JsonProperty("ethLiquidity")]
        public decimal EthLiquidity { get; set; }
        
        [JsonProperty("ethVolume")]
        public decimal EthVolume { get; set; }
        
        [JsonProperty("marginalEthRate")]
        public decimal MarginalEthRate { get; set; }
        
        [JsonProperty("tokenLiquidity")]
        public decimal TokenLiquidity { get; set; }
    }
}