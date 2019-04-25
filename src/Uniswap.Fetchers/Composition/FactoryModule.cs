using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Hosting;
using Nethereum.Contracts;
using Uniswap.Data.Mongo.Repositories;
using Uniswap.Data.Repositories;
using Uniswap.Fetchers.Core;
using Uniswap.Fetchers.Core.Factory;
using Uniswap.Fetchers.Core.Factory.Impl;
using Uniswap.Fetchers.HostedServices;
using Uniswap.Fetchers.Infrastructure.Mongo.Factory;
using Uniswap.Fetchers.Options;
using Uniswap.SmartContracts.Factory.CQS;
using Uniswap.SmartContracts.Factory.Service;

namespace Uniswap.Fetchers.Composition
{
    public class FactoryModule : Module
    {
        private readonly string _factoryAddress;
        private readonly FetcherOptions _fetcherOptions;

        public FactoryModule(
            string factoryAddress,
            FetcherOptions fetcherOptions)
        {
            _factoryAddress = factoryAddress;
            _fetcherOptions = fetcherOptions;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FactoryService>()
                .WithParameter(new TypedParameter(typeof(string), _factoryAddress))
                .SingleInstance();

            builder
                .RegisterType<EventsFetcher<NewExchangeEventDTO>>()
                .WithParameter(new ResolvedParameter(
                    (pi, cc) => pi.ParameterType == typeof(Event<NewExchangeEventDTO>),
                    (pi, cc) => cc.Resolve<FactoryService>().ContractHandler.GetEvent<NewExchangeEventDTO>()))
                .As<IEventsFetcher<NewExchangeEventDTO>>();

            builder
                .RegisterType<ExchangeMapper>()
                .As<IExchangeMapper>();

            builder
                .RegisterType<StartBlockProvider>()
                .As<IStartBlockProvider>();

            builder
                .RegisterType<MongoExchangeEntityFactory>()
                .As<IExchangeEntityFactory>();

            builder
                .RegisterType<ExchangeProcessor>()
                .As<IExchangeProcessor>();

            builder
                .Register(cc => new FetcherSettings
                {
                    BlocksPerIteration = _fetcherOptions.BlocksPerIteration,
                    RecentBlockReachLimit = _fetcherOptions.RecentBlockReachLimit,
                    DelayMs = _fetcherOptions.DelayMs
                });
            
            builder
                .RegisterType<MongoExchangeRepository>()
                .As<IExchangeRepository>();

            builder
                .RegisterType<FactoryFetcher>()
                .Keyed<IFetcher>(nameof(FactoryFetcher));

            builder
                .RegisterType<FactoryHostedService>()
                .WithParameter(new ResolvedParameter(
                    (pi, cc) => pi.ParameterType == typeof(IFetcher),
                    (pi, cc) => cc.ResolveKeyed<IFetcher>(nameof(FactoryFetcher))))
                .As<IHostedService>();

            base.Load(builder);
        }
    }
}