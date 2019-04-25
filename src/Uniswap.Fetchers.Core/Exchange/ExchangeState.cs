using Uniswap.Data.Entities;

namespace Uniswap.Fetchers.Core.Exchange
{
    public class ExchangeState
    {
        public decimal EthLiquidity { get; set; }
        public decimal TokenLiquidity { get; set; }

        public void Update(IExchangeEventEntity entity)
        {
            switch (entity.Type)
            {
                case ExchangeEventType.TokenPurchase:
                    EthLiquidity = EthLiquidity + entity.EthAmount;
                    TokenLiquidity = TokenLiquidity - entity.TokenAmount;
                    break;
                case ExchangeEventType.EthPurchase:
                    EthLiquidity = EthLiquidity - entity.EthAmount;
                    TokenLiquidity = TokenLiquidity + entity.TokenAmount;
                    break;
                case ExchangeEventType.AddLiquidity:
                    EthLiquidity = EthLiquidity + entity.EthAmount;
                    TokenLiquidity = TokenLiquidity + entity.TokenAmount;
                    break;
                case ExchangeEventType.RemoveLiquidity:
                    EthLiquidity = EthLiquidity - entity.EthAmount;
                    TokenLiquidity = TokenLiquidity - entity.TokenAmount;
                    break;
            }
        }
    }
}