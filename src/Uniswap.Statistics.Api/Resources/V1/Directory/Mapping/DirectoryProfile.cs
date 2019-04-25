using System.Collections.Generic;
using AutoMapper;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Resources.V1.Directory.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Directory.Mapping
{
    public class DirectoryProfile : Profile
    {
        public DirectoryProfile()
        {
            CreateMap<IExchangeEntity, DirectoryDto>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.TokenName))
                .ForMember(dest => dest.Symbol, m => m.MapFrom(src => src.TokenSymbol))
                .ForMember(dest => dest.ExchangeAddress, m => m.MapFrom(src => src.Id));

            CreateMap<List<IExchangeEntity>, DirectoriesDto>()
                .ForMember(dest => dest.Exchanges, m => m.MapFrom(src => src));
        }
    }
}