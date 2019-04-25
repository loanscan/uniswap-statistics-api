using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Uniswap.Fetchers.Core
{
    public interface IEventsFetcherFactory
    {
        IEventsFetcher<TEventDto> Create<TEventDto>()
            where TEventDto : IEventDTO, new();
    }
}