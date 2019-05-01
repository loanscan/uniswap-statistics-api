using Autofac;
using MongoDB.Driver;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Data.Mongo.Indexes;
using Uniswap.Data.Mongo.Repositories;
using Uniswap.Data.Repositories;
using Uniswap.Statistics.Api.Options;

namespace Uniswap.Statistics.Api.Composition
{
    public class MongoModule : Module
    {
        private readonly DbOptions _dbOptions;

        public MongoModule(DbOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            var db = new MongoClient(_dbOptions.ConnectionString).GetDatabase(_dbOptions.DatabaseName);

            builder
                .RegisterInstance(db.GetCollection<MongoExchangeEntity>(_dbOptions.ExchangesCollectionName));
            
            builder
                .RegisterInstance(db.GetCollection<MongoExchangeEventEntity>(_dbOptions.ExchangeEventsCollectionName));

            builder
                .RegisterType<MongoExchangeRepository>()
                .As<IExchangeRepository>();

            builder
                .RegisterType<MongoExchangeEventsRepository>()
                .As<IExchangeEventsRepository>();

            builder.RegisterType<MongoExchangeEventsIndexInitializer>()
                .OnActivated(async e => await e.Instance.Initialize())
                .SingleInstance();

            builder.RegisterBuildCallback(registry => registry.Resolve<MongoExchangeEventsIndexInitializer>());
            
            base.Load(builder);
        }
    }
}