using Autofac;
using Uniswap.Statistics.Api.Core.Stats;
using Uniswap.Statistics.Api.Core.Stats.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class StatsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatsService>()
                .As<IStatsService>();
            
            base.Load(builder);
        }
    }
}