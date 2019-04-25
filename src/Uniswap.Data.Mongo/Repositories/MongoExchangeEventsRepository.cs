using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Data.Mongo.Repositories
{
    public class MongoExchangeEventsRepository : IExchangeEventsRepository
    {
        private readonly IMongoCollection<MongoExchangeEventEntity> _collection;

        public MongoExchangeEventsRepository(IMongoCollection<MongoExchangeEventEntity> collection)
        {
            _collection = collection;
        }

        public async Task AddOrUpdateAsync(IExchangeEventEntity entity)
        {
            await _collection.FindOneAndReplaceAsync<MongoExchangeEventEntity>(
                e => e.Id.Equals(entity.Id), (MongoExchangeEventEntity) entity,
                new FindOneAndReplaceOptions<MongoExchangeEventEntity> {IsUpsert = true});
        }

        public async Task<IEnumerable<IExchangeEventEntity>> GetForLastDayAsync()
        {
            var now = DateTime.UtcNow;
            var filter = Builders<MongoExchangeEventEntity>.Filter.And(
                Builders<MongoExchangeEventEntity>.Filter.Gte(entity => entity.Timestamp, now.Subtract(TimeSpan.FromDays(1))),
                Builders<MongoExchangeEventEntity>.Filter.Lte(entity => entity.Timestamp, now));

            var result = await _collection.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<IExchangeEventEntity>> GetForLastDayByExchangeAddressAsync(string address)
        {
            var now = DateTime.UtcNow;
            var filter = Builders<MongoExchangeEventEntity>.Filter.And(
                Builders<MongoExchangeEventEntity>.Filter.Gte(entity => entity.Timestamp, now.Subtract(TimeSpan.FromDays(1))),
                Builders<MongoExchangeEventEntity>.Filter.Lte(entity => entity.Timestamp, now),
                Builders<MongoExchangeEventEntity>.Filter.Eq(entity => entity.ExchangeAddress, address));

            var result = await _collection.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<IExchangeEventEntity>> GetByDateRangeAsync(string exchangeAddress, DateTime start, DateTime end)
        {
            var filter = Builders<MongoExchangeEventEntity>.Filter.And(
                Builders<MongoExchangeEventEntity>.Filter.Gte(entity => entity.Timestamp, start),
                Builders<MongoExchangeEventEntity>.Filter.Lte(entity => entity.Timestamp, end),
                Builders<MongoExchangeEventEntity>.Filter.Eq(entity => entity.ExchangeAddress, exchangeAddress)
            );

            var result = _collection.Find(filter);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<IExchangeEventEntity>> GetSortedByDateRangeAsync(string exchangeAddress, DateTime start, DateTime end, int limit)
        {
            var filter = Builders<MongoExchangeEventEntity>.Filter.And(
                Builders<MongoExchangeEventEntity>.Filter.Gte(entity => entity.Timestamp, start),
                Builders<MongoExchangeEventEntity>.Filter.Lte(entity => entity.Timestamp, end),
                Builders<MongoExchangeEventEntity>.Filter.Eq(entity => entity.ExchangeAddress, exchangeAddress)
            );

            var result = _collection.Aggregate()
                .Match(filter)
                .SortByDescending(x => x.BlockNumber)
                .ThenByDescending(x => x.LogIndex)
                .Limit(limit);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<IExchangeEventEntity>> FindByAsync(string userAddress, string exchangeAddress)
        {
            var filter = Builders<MongoExchangeEventEntity>.Filter.And(
                Builders<MongoExchangeEventEntity>.Filter.Eq(entity => entity.ExchangeAddress, exchangeAddress),
                Builders<MongoExchangeEventEntity>.Filter.Eq(entity => entity.CallerAddress, userAddress)
            );
            
            var result = await _collection.FindAsync(filter);
            return await result.ToListAsync();
        }
    }
}