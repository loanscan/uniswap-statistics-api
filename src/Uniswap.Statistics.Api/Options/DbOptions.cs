namespace Uniswap.Statistics.Api.Options
{
    public class DbOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ExchangesCollectionName { get; set; }
        public string ExchangeEventsCollectionName { get; set; }
    }
}