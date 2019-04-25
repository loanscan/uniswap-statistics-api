using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Data.Mongo.Repositories
{
    public class MongoExchangeRepository : IExchangeRepository
    {
        private readonly IMongoCollection<MongoExchangeEntity> _collection;

        public MongoExchangeRepository(IMongoCollection<MongoExchangeEntity> collection)
        {
            _collection = collection;
        }

        public async Task AddOrUpdate(IExchangeEntity entity)
        {
            await _collection.FindOneAndReplaceAsync<MongoExchangeEntity>(
                e => e.Id.Equals(entity.Id),
                (MongoExchangeEntity) entity,
                new FindOneAndReplaceOptions<MongoExchangeEntity> {IsUpsert = true});
        }

        public async Task<IExchangeEntity> FindByAsync(string symbol = null, string tokenAddress = null,
            string exchangeAddress = null)
        {
            var exchange =
                await (await _collection.FindAsync(
                        e => e.TokenSymbol.Equals(symbol) || e.TokenAddress.Equals(tokenAddress) ||
                             e.Id.Equals(exchangeAddress), new FindOptions<MongoExchangeEntity>()))
                    .FirstOrDefaultAsync();

            return exchange;
        }

        public async Task<IEnumerable<IExchangeEntity>> GetAllAsync()
        {
            return await _collection.Find(e => true).ToListAsync();
        }

        public async Task<IExchangeEntity> GetLastCreatedAsync()
        {
            return await _collection.Aggregate().SortByDescending(e => e.BlockNumber).FirstOrDefaultAsync();
        }

        public async Task Update(string exchangeId, decimal ethLiquidity, decimal tokenLiquidity, decimal totalSupply)
        {
            var update = Builders<MongoExchangeEntity>.Update
                .Set(e => e.EthLiquidity, ethLiquidity)
                .Set(e => e.TokenLiquidity, tokenLiquidity)
                .Set(e => e.TotalSupply, totalSupply);
            await _collection.FindOneAndUpdateAsync(e => e.Id.Equals(exchangeId), update);
        }
    }
}