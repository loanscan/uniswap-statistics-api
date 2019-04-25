using Autofac;
using MongoDB.Driver;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Fetchers.Options;

namespace Uniswap.Fetchers.Composition
{
    public class MongoModule : Module
    {
        private readonly DbOptions _options;

        public MongoModule(DbOptions options)
        {
            _options = options;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            var db = new MongoClient(_options.ConnectionString).GetDatabase(_options.DatabaseName);

            builder
                .RegisterInstance(db.GetCollection<MongoExchangeEntity>(_options.ExchangesCollectionName));

            builder
                .RegisterInstance(db.GetCollection<MongoExchangeEventEntity>(_options.ExchangeEventsCollectionName));
            
            builder
                .RegisterInstance(db.GetCollection<MongoLastBlockHandledByExchangeEntity>(_options.LastBlockFetchedByExchangeCollectionName));
            
            base.Load(builder);
        }
    }
}