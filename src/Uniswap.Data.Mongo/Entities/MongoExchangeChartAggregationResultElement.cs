using System;
using MongoDB.Bson.Serialization.Attributes;
using Uniswap.Data.AggregationResults;

namespace Uniswap.Data.Mongo.Entities
{
    public class MongoExchangeChartAggregationResultElement : IExchangeChartAggregationResultElement
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