using System;
using MongoDB.Bson.Serialization.Attributes;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Mongo.Entities
{
    public class MongoExchangeChartEntity : IExchangeChartEntity
    {
        [BsonId]
        public DateTime Id { get; set; }
        [BsonElement("ethLiquidity")]
        public decimal EthLiquidity { get; set; }
        [BsonElement("tokenLiquidity")]
        public decimal TokenLiquidity { get; set; }
        [BsonElement("ethVolume")]
        public decimal EthVolume { get; set; }
    }
}