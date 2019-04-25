using Autofac;
using Uniswap.Statistics.Api.Core.Exchange;
using Uniswap.Statistics.Api.Core.Exchange.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class ExchangeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExchangeService>()
                .As<IExchangeService>();
            
            base.Load(builder);
        }
    }
}