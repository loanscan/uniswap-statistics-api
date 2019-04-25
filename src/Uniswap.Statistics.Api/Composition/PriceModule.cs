using Autofac;
using Uniswap.Statistics.Api.Core.Price;
using Uniswap.Statistics.Api.Core.Price.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class PriceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PriceService>()
                .As<IPriceService>();
            
            base.Load(builder);
        }
    }
}