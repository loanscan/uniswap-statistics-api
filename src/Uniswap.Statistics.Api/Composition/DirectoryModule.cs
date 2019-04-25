using Autofac;
using Uniswap.Statistics.Api.Core.Directory;
using Uniswap.Statistics.Api.Core.Directory.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class DirectoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DirectoryService>()
                .As<IDirectoryService>();
            
            base.Load(builder);
        }
    }
}