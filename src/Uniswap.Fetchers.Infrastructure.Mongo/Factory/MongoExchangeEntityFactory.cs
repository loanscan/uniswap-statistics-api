using Uniswap.Data.Entities;
using Uniswap.Data.Mongo.Entities;
using Uniswap.Fetchers.Core.Factory;
using Uniswap.Fetchers.Core.Token;

namespace Uniswap.Fetchers.Infrastructure.Mongo.Factory
{
    public class MongoExchangeEntityFactory : IExchangeEntityFactory
    {
        public IExchangeEntity Create(
            string exchangeAddress,
            string tokenAddress,
            TokenInfo tokenInfo,
            ulong blockNumber,
            decimal ethLiquidity,
            decimal tokenLiquidity,
            decimal totalSupply,
            string theme)
        {
            return new MongoExchangeEntity
            {
                Id = exchangeAddress,
                TokenAddress = tokenAddress,
                TokenName = tokenInfo.Name,
                TokenSymbol = tokenInfo.Symbol,
                TokenDecimals = tokenInfo.Decimals,
                BlockNumber = blockNumber,
                EthLiquidity = ethLiquidity,
                TokenLiquidity = tokenLiquidity,
                TotalSupply = totalSupply,
                Theme = theme
            };
        }
    }
}