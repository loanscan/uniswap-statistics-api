using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.History.Dtos
{
    public class EventDto
    {
        [JsonProperty("tx")]
        public string Tx { get; set; }
        
        [JsonProperty("user")]
        public string User { get; set; }
        
        [JsonProperty("block")]
        public ulong Block { get; set; }
        
        [JsonProperty("ethAmount")]
        public decimal EthAmount { get; set; }
        
        [JsonProperty("tokenAmount")]
        public decimal TokenAmount { get; set; }
        
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        
        [JsonProperty("event")]
        public string Event { get; set; }
        
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}