using MongoDB.Bson.Serialization.Attributes;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Mongo.Entities
{
    public class MongoLastBlockHandledByExchangeEntity : ILastBlockFetchedByExchangeEntity
    {
        [BsonId]
        public string Id { get; set; }
        
        [BsonElement("lastBlock")]
        public ulong LastBlock { get; set; }
    }
}