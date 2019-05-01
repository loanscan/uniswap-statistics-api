using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Uniswap.Data.AggregationResults;
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

        public async Task<IEnumerable<IExchangeChartAggregationResultElement>> GetChartsAsync(string exchangeAddress, DateTime start, DateTime end, int chartIntervalUnit)
        {
            var aggregation = _collection.Aggregate()
                .Match(x =>
                    x.ExchangeAddress == exchangeAddress && x.Timestamp >= start && x.Timestamp <= end)
                .Project(entity => new
                {
                    entity.Timestamp,
                    entity.EthLiquidityAfterEvent,
                    entity.TokenLiquidityAfterEvent,
                    EthAmount = entity.Type == ExchangeEventType.EthPurchase ||
                                entity.Type == ExchangeEventType.TokenPurchase
                        ? entity.EthAmount
                        : 0
                })
                .SortBy(x => x.Timestamp);

            IAggregateFluent<MongoExchangeChartAggregationResultElement> aggregateFluent;

            switch (chartIntervalUnit)
            {
                case 0:
                    aggregateFluent = aggregation.Group(
                        arg => new DateTime(arg.Timestamp.Year, arg.Timestamp.Month, arg.Timestamp.Day),
                        grouping => new MongoExchangeChartAggregationResultElement
                        {
                            EthLiquidity = grouping.Last().EthLiquidityAfterEvent,
                            TokenLiquidity = grouping.Last().TokenLiquidityAfterEvent,
                            EthVolume = grouping.Sum(x => x.EthAmount),
                            Id = grouping.Key
                        });
                    break;
                case 1:
                    aggregateFluent = aggregation.Group(
                        arg => new DateTime(arg.Timestamp.Year, arg.Timestamp.Month, 1),
                        grouping => new MongoExchangeChartAggregationResultElement
                        {
                            EthLiquidity = grouping.Last().EthLiquidityAfterEvent,
                            TokenLiquidity = grouping.Last().TokenLiquidityAfterEvent,
                            EthVolume = grouping.Sum(x => x.EthAmount),
                            Id = grouping.Key
                        });
                    break;
                case 2:
                    aggregateFluent = aggregation.Group(
                        arg => new DateTime(arg.Timestamp.Year, 1, 1),
                        grouping => new MongoExchangeChartAggregationResultElement
                        {
                            EthLiquidity = grouping.Last().EthLiquidityAfterEvent,
                            TokenLiquidity = grouping.Last().TokenLiquidityAfterEvent,
                            EthVolume = grouping.Sum(x => x.EthAmount),
                            Id = grouping.Key
                        });
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(chartIntervalUnit));
            }

            return await aggregateFluent.SortBy(x => x.Id).ToListAsync();
        }
    }
}