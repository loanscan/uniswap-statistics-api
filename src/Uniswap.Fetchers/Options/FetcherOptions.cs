namespace Uniswap.Fetchers.Options
{
    public class FetcherOptions
    {
        public ulong BlocksPerIteration { get; set; }
        public ulong RecentBlockReachLimit { get; set; }
        public int DelayMs { get; set; }
    }
}