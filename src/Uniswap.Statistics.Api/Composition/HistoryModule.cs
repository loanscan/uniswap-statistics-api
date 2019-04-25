using Autofac;
using Uniswap.Statistics.Api.Core.History;
using Uniswap.Statistics.Api.Core.History.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class HistoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HistoryService>()
                .As<IHistoryService>();
        
            base.Load(builder);
        }
    }
}