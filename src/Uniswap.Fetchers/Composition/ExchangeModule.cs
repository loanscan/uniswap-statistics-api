using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Hosting;
using Uniswap.Data.Mongo.Repositories;
using Uniswap.Data.Repositories;
using Uniswap.Fetchers.Core;
using Uniswap.Fetchers.Core.Exchange;
using Uniswap.Fetchers.Core.Exchange.Impl;
using Uniswap.Fetchers.HostedServices;
using Uniswap.Fetchers.Infrastructure.Mongo.Exchange;
using Uniswap.Fetchers.Options;
using Uniswap.SmartContracts.Exchange.Service;

namespace Uniswap.Fetchers.Composition
{
    public class ExchangeModule : Module
    {
        private readonly ExchangeFetcherOptions _exchangeFetcherOptions;
        private readonly ExchangeThemesOptions _exchangeThemesOptions;

        public ExchangeModule(
            ExchangeFetcherOptions exchangeFetcherOptions,
            ExchangeThemesOptions exchangeThemesOptions)
        {
            _exchangeFetcherOptions = exchangeFetcherOptions;
            _exchangeThemesOptions = exchangeThemesOptions;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExchangeService>();
            
            builder
                .RegisterType<ExchangeGateway>()
                .As<IExchangeGateway>();

            builder
                .RegisterType<MongoExchangeEventEntityFactory>()
                .As<IExchangeEventEntityFactory>();

            builder
                .RegisterType<ExchangeEventMapper>()
                .As<IExchangeEventMapper>();

            builder
                .RegisterType<StartBlockService>()
                .As<IStartBlockService>();

            builder
                .RegisterType<ExchangeEventsFetcher>();

            builder
                .RegisterType<MongoLastBlockFetchedByExchangeEntityFactory>()
                .As<ILastBlockFetchedByExchangeEntityFactory>();

            builder
                .RegisterType<MongoLastBlockFetchedByExchangeRepository>()
                .As<ILastBlockFetchedByExchangeRepository>();
            
            builder
                .RegisterType<MongoExchangeEventsRepository>()
                .As<IExchangeEventsRepository>();
            
            builder
                .RegisterType<ExchangeEventsProcessor>()
                .As<IExchangeEventsProcessor>();

            builder
                .RegisterType<MongoExchangeEventsRepository>()
                .As<IExchangeEventsRepository>();
            
            builder
                .Register(cc => new ExchangeFetcherSettings
                {
                    BlocksPerIteration = _exchangeFetcherOptions.BlocksPerIteration,
                    RecentBlockReachLimit = _exchangeFetcherOptions.RecentBlockReachLimit,
                    DelayMs = _exchangeFetcherOptions.DelayMs,
                    UpdateExchangesIntervalMs = _exchangeFetcherOptions.UpdateExchangesIntervalMs
                });

            builder
                .RegisterType<ExchangeTotalSupplyProvider>()
                .As<IExchangeTotalSupplyProvider>();

            builder
                .RegisterType<ExchangeThemeProvider>()
                .WithParameter(new TypedParameter(typeof(Dictionary<string, string>), _exchangeThemesOptions))
                .As<IExchangeThemeProvider>();

            builder
                .RegisterType<ExchangeServiceEventsFetcherFactory>()
                .As<IEventsFetcherFactory>();
            
            builder
                .RegisterType<ExchangeFetcher>()
                .Keyed<IFetcher>(nameof(ExchangeFetcher));
            
            builder
                .RegisterType<ExchangesHostedService>()
                .WithParameter(new ResolvedParameter(
                    (pi, cc) => pi.ParameterType == typeof(IFetcher),
                    (pi, cc) => cc.ResolveKeyed<IFetcher>(nameof(ExchangeFetcher))))
                .As<IHostedService>();
            
            base.Load(builder);
        }
    }
}