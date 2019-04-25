using AutoMapper;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Resources.V1.Exchange.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Exchange.Mapping
{
    public class ExchangeProfile : Profile
    {
        public ExchangeProfile()
        {
            CreateMap<IExchangeEntity, ExchangeDto>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.TokenName))
                .ForMember(dest => dest.Symbol, m => m.MapFrom(src => src.TokenSymbol))
                .ForMember(dest => dest.Fee, m => m.MapFrom(src => UniswapUtils.Fee))
                .ForMember(dest => dest.Version, m => m.MapFrom(src => UniswapUtils.Version))
                .ForMember(dest => dest.ExchangeAddress, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.EthDecimals, m => m.MapFrom(src => EthUtils.Decimals))
                .ForMember(dest => dest.Price, m => m.MapFrom(src => UniswapUtils.CalculateMarginalRate(src.EthLiquidity, src.TokenLiquidity)));
        }
    }
}