using Uniswap.Data.Entities;

namespace Uniswap.Statistics.Api.Core.History
{
    public class ExchangeEvent
    {
        public string Tx { get; set; }
        public string User { get; set; }
        public ulong Block { get; set; }
        public decimal EthAmount { get; set; }
        public decimal TokenAmount { get; set; }
        public decimal Fee { get; set; }
        public ExchangeEventType Event { get; set; }
        public long Timestamp { get; set; }
    }
}