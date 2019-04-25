using Autofac;
using Uniswap.Statistics.Api.Core.Chart;
using Uniswap.Statistics.Api.Core.Chart.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class ChartModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChartService>()
                .As<IChartService>();
        
            base.Load(builder);
        }
    }
}