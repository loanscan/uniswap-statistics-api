using System;
using Autofac;
using Uniswap.Fetchers.Core.Eth;
using Uniswap.Fetchers.Core.Eth.Impl;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.Fetchers.Core.Infra.Impl;
using Uniswap.Fetchers.Core.Token;
using Uniswap.Fetchers.Core.Token.Impl;
using Uniswap.Fetchers.Core.Token.Impl.Ethplorer;
using Uniswap.Fetchers.Options;
using Uniswap.SmartContracts.DSErc20.Service;
using Uniswap.SmartContracts.Erc20.Service;

namespace Uniswap.Fetchers.Composition
{
    public class InfraModule : Module
    {
        private readonly TokenInfoProviderOptions _tokenInfoProviderOptions;

        public InfraModule(TokenInfoProviderOptions tokenInfoProviderOptions)
        {
            _tokenInfoProviderOptions = tokenInfoProviderOptions;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PollyRetrier>()
                .As<IRetrier>();

            builder
                .RegisterType<EthGateway>()
                .As<IEthGateway>();

            builder
                .RegisterType<EthLiquidityProvider>()
                .As<IEthLiquidityProvider>();

            builder
                .RegisterType<Erc20Service>();

            builder
                .RegisterType<Erc20Gateway>()
                .As<IErc20Gateway>();

            builder
                .RegisterType<FromErc20TokenLiquidityProvider>()
                .As<ITokenLiquidityProvider>();

            switch (_tokenInfoProviderOptions.Source)
            {
                case TokenInfoProviderSource.Erc20:
                    builder
                        .RegisterType<DSErc20Service>();
                    builder
                        .RegisterType<DSErc20Gateway>()
                        .As<IDSErc20Gateway>();
                    builder
                        .RegisterType<FromErc20TokenInfoProvider>()
                        .As<ITokenInfoProvider>()
                        .SingleInstance();
                    break;
                case TokenInfoProviderSource.Ethplorer:
                    builder
                        .Register(_ => new EthplorerSettings
                        {
                            Url = _tokenInfoProviderOptions.Ethplorer.Url,
                            ApiKey = _tokenInfoProviderOptions.Ethplorer.ApiKey
                        });
                    builder
                        .RegisterType<EthplorerApi>()
                        .SingleInstance();
                    builder
                        .RegisterType<EthplorerTokenInfoProvider>()
                        .As<ITokenInfoProvider>()
                        .SingleInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_tokenInfoProviderOptions.Source),
                        _tokenInfoProviderOptions.Source, null);
            }

            builder
                .RegisterDecorator<TokenInfoProviderCacheDecorator, ITokenInfoProvider>();

            builder
                .RegisterType<EthRecentBlockProvider>()
                .As<IRecentBlockProvider>();

            builder
                .RegisterType<BlockTimestampProvider>()
                .As<IBlockTimestampProvider>();

            base.Load(builder);
        }
    }
}