namespace Uniswap.Statistics.Api.Core.User
{
    public class UserInfo
    {
        public decimal PoolTotalSupply { get; set; }
        public decimal UserNumPoolTokens { get; set; }
        public decimal UserPoolPercent { get; set; }
        public decimal UserEthLiquidity { get; set; }
        public decimal EthFees { get; set; }
        public decimal UserTokenLiquidity { get; set; }
        public decimal TokenFees { get; set; }
    }
}