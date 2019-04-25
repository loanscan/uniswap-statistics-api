using Newtonsoft.Json;

namespace Uniswap.Statistics.Api.Resources.V1.Directory.Dtos
{
    public class DirectoriesDto
    {
        [JsonProperty("exchanges")]
        public DirectoryDto[] Exchanges { get; set; }
    }
}