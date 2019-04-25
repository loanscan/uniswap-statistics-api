using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Mongo.Entities
{
    public class MongoExchangeEntity : IExchangeEntity
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("tokenAddress")]
        public string TokenAddress { get; set; }
        
        [BsonElement("tokenName")]
        public string TokenName { get; set; }
        
        [BsonElement("tokenSymbol")]
        public string TokenSymbol { get; set; }
        
        [BsonElement("tokenDecimals")]
        public int TokenDecimals { get; set; }
        
        [BsonElement("blockNumber")]
        public ulong BlockNumber { get; set; }
        
        [BsonElement("ethLiquidity")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EthLiquidity { get; set; }
        
        [BsonElement("tokenLiquidity")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TokenLiquidity { get; set; }
        
        [BsonElement("totalSupply")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TotalSupply { get; set; }

        [BsonElement("theme")]
        public string Theme { get; set; }
    }
}