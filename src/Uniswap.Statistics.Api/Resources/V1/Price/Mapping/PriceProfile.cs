using AutoMapper;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Statistics.Api.Resources.V1.Price.Dtos;

namespace Uniswap.Statistics.Api.Resources.V1.Price.Mapping
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<IExchangeEntity, PriceDto>()
                .ForMember(dest => dest.Symbol, m => m.MapFrom(src => src.TokenSymbol))
                .ForMember(dest => dest.Price,
                    m => m.MapFrom(src => UniswapUtils.CalculateMarginalRate(src.EthLiquidity, src.TokenLiquidity)))
                .ForMember(dest => dest.InvPrice,
                    m => m.MapFrom(src => UniswapUtils.CalculateInvMarginRate(src.EthLiquidity, src.TokenLiquidity)));
        }
    }
}