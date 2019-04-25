using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Stats.Dtos
{
    public class StatsExchangesDto
    {
        [JsonProperty("exchanges")]
        public StatsExchangeDto[] Exchanges { get; set; }
    }
}