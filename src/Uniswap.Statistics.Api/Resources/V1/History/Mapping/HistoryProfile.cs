using AutoMapper;
using Uniswap.Statistics.Api.Core.History;
using Uniswap.Statistics.Api.Resources.V1.History.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.History.Mapping
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<ExchangeEvent, EventDto>()
                .ForMember(dest => dest.Event, m => m.MapFrom(src => src.Event));
        }
    }
}