using System.Threading.Tasks;
using MongoDB.Driver;
using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Data.Mongo.Repositories
{
    public class MongoLastBlockFetchedByExchangeRepository : ILastBlockFetchedByExchangeRepository
    {
        private readonly IMongoCollection<MongoLastBlockHandledByExchangeEntity> _collection;

        public MongoLastBlockFetchedByExchangeRepository(IMongoCollection<MongoLastBlockHandledByExchangeEntity> collection)
        {
            _collection = collection;
        }

        public async Task<ILastBlockFetchedByExchangeEntity> FindByIdAsync(string id)
        {
            var entity =
                await (await _collection.FindAsync(e => e.Id.Equals(id),
                    new FindOptions<MongoLastBlockHandledByExchangeEntity>())).FirstOrDefaultAsync();

            return entity;
        }

        public async Task AddOrUpdateAsync(ILastBlockFetchedByExchangeEntity entity)
        {
            await _collection.FindOneAndReplaceAsync<MongoLastBlockHandledByExchangeEntity>(
                e => e.Id.Equals(entity.Id),
                (MongoLastBlockHandledByExchangeEntity) entity,
                new FindOneAndReplaceOptions<MongoLastBlockHandledByExchangeEntity> {IsUpsert = true});
        }
    }
}