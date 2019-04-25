using System.Collections.Generic;
using AutoMapper;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Resources.V1.Stats.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Stats.Mapping
{
    public class StatsProfile : Profile
    {
        public StatsProfile()
        {
            CreateMap<IExchangeEntity, StatsExchangeDto>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.TokenName))
                .ForMember(dest => dest.Symbol, m => m.MapFrom(src => src.TokenSymbol))
                .ForMember(dest => dest.Erc20Liquidity, m => m.MapFrom(src => src.TokenLiquidity));

            CreateMap<List<IExchangeEntity>, StatsExchangesDto>()
                .ForMember(dest => dest.Exchanges, m => m.MapFrom(src => src));
        }
    }
}