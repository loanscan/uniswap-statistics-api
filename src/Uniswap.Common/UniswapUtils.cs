namespace Uniswap.Common
{
    public static class UniswapUtils
    {
        public const decimal Fee = 0.003m;
        public const int Version = 1;
        
        public static decimal CalculateMarginalRate(decimal ethLiquidity, decimal tokenLiquidity)
        {
            if (ethLiquidity != 0)
            {
                return tokenLiquidity / ethLiquidity;
            }
            else
            {
                return 0;
            }
        }

        public static decimal CalculateInvMarginRate(decimal ethLiquidity, decimal tokenLiquidity)
        {
            var rate = CalculateMarginalRate(ethLiquidity, tokenLiquidity);

            if (rate != 0)
            {
                return 1 / rate;
            }
            else
            {
                return 0;
            }
        }
    }
}