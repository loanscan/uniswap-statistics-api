namespace Uniswap.Fetchers.Options
{
    public class Options
    {
        public string NodeUrl { get; set; }
        public ContractsOptions Contracts { get; set; }
        public DbOptions Db { get; set; }
        public FetcherOptions Fetcher { get; set; }
        public ExchangeFetcherOptions ExchangeFetcher { get; set; }
        public TokenInfoProviderOptions TokenInfoProvider { get; set; }
        public ExchangeThemesOptions ExchangeThemes { get; set; }
    }
}