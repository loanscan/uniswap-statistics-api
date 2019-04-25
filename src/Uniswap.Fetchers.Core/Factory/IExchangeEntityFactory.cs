using Uniswap.Data.Entities;
using Uniswap.Fetchers.Core.Token;

namespace Uniswap.Fetchers.Core.Factory
{
    public interface IExchangeEntityFactory
    {
        IExchangeEntity Create(string exchangeAddress, string tokenAddress, TokenInfo tokenInfo, ulong blockNumber,
            decimal ethLiquidity, decimal tokenLiquidity, decimal totalSupply, string theme);
    }
}