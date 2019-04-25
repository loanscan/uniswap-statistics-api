using Autofac;
using Uniswap.Statistics.Api.Core.User;
using Uniswap.Statistics.Api.Core.User.Impl;

namespace Uniswap.Statistics.Api.Composition
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserService>()
                .As<IUserService>();
        
            base.Load(builder);
        }
    }
}