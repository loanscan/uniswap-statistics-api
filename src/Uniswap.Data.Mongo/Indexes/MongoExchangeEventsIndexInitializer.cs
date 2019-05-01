using System.Threading.Tasks;
using MongoDB.Driver;
using Uniswap.Data.Mongo.Entities;

namespace Uniswap.Data.Mongo.Indexes
{
    public class MongoExchangeEventsIndexInitializer
    {
        private readonly IMongoCollection<MongoExchangeEventEntity> _collection;

        public MongoExchangeEventsIndexInitializer(IMongoCollection<MongoExchangeEventEntity> collection)
        {
            _collection = collection;
        }

        public async Task Initialize()
        {       
            var indexBuilder = Builders<MongoExchangeEventEntity>.IndexKeys;
            
            await _collection.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoExchangeEventEntity>(indexBuilder.Ascending(x => x.Timestamp)));

            await _collection.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoExchangeEventEntity>(indexBuilder.Ascending(x => x.ExchangeAddress)));
        }
    }
}