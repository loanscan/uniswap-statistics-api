using Autofac;
using Nethereum.Web3;

namespace Uniswap.Fetchers.Composition
{
    public class Web3Module : Module
    {
        private readonly string _nodeUrl;

        public Web3Module(string nodeUrl)
        {
            _nodeUrl = nodeUrl;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(new Web3(_nodeUrl));
            
            base.Load(builder);
        }
    }
}