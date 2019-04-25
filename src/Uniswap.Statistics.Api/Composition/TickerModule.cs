using Autofac;
using Uniswap.Statistics.Api.Core.Ticker;
using Uniswap.Statistics.Api.Core.Ticker.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class TickerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TickerService>()
                .As<ITickerService>();
        
            base.Load(builder);
        }
    }
}