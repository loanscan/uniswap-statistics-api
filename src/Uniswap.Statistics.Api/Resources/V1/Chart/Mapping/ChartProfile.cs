using AutoMapper;
using Uniswap.Statistics.Api.Resources.V1.Chart.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Chart.Mapping
{
    public class ChartProfile : Profile
    {
        public ChartProfile()
        {
            CreateMap<Core.Chart.Chart, ChartDto>()
                .ForMember(dest => dest.Date, m => m.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));
        }
    }
}