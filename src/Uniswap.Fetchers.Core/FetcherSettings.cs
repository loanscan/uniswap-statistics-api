namespace Uniswap.Fetchers.Core
{
    public class FetcherSettings
    {
        public ulong BlocksPerIteration { get; set; }
        public ulong RecentBlockReachLimit { get; set; }
        public int DelayMs { get; set; }
    }
}