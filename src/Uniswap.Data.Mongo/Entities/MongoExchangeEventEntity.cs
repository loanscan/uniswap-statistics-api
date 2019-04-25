using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Uniswap.Data.Entities;

namespace Uniswap.Data.Mongo.Entities
{
    public class MongoExchangeEventEntity : IExchangeEventEntity
    {
        [BsonId]
        public string Id { get; set; }
        
        [BsonElement("exchangeAddress")]
        public string ExchangeAddress { get; set; }
        
        [BsonElement("callerAddress")]
        public string CallerAddress { get; set; }
        
        [BsonElement("type")]
        public ExchangeEventType Type { get; set; }
        
        [BsonElement("ethAmount")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EthAmount { get; set; }
        
        [BsonElement("tokenAmount")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TokenAmount { get; set; }
        
        [BsonElement("ethLiquidityBeforeEvent")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EthLiquidityBeforeEvent { get; set; }
        
        [BsonElement("ethLiquidityAfterEvent")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EthLiquidityAfterEvent { get; set; }
        
        [BsonElement("tokenLiquidityBeforeEvent")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TokenLiquidityBeforeEvent { get; set; }
        
        [BsonElement("tokenLiquidityAfterEvent")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TokenLiquidityAfterEvent { get; set; }
        
        [BsonElement("txHash")]
        public string TxHash { get; set; }
        
        [BsonElement("logIndex")]
        public int LogIndex { get; set; }
        
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
        
        [BsonElement("ethFee")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EthFee { get; set; }
        
        [BsonElement("tokenFee")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TokenFee { get; set; }
        
        [BsonElement("tokenAddress")]
        public string TokenAddress { get; set; }
        
        [BsonElement("blockNumber")]
        [BsonRepresentation(BsonType.Int64)]
        public ulong BlockNumber { get; set; }
        
        [BsonElement("callerBalance")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal CallerBalance { get; set; }

    }
}