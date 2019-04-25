using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;

namespace Uniswap.Statistics.Api.Core.History.Impl
{
    public class HistoryService : IHistoryService
    {
        private readonly IExchangeEventsRepository _exchangeEventsRepository;

        public HistoryService(IExchangeEventsRepository exchangeEventsRepository)
        {
            _exchangeEventsRepository = exchangeEventsRepository;
        }

        public async Task<IEnumerable<ExchangeEvent>> GetByExchangeAddress(string address, long startTime, long endTime, int count)
        {
            var start = startTime.ToUnixUtcDateTime();
            var end = endTime.ToUnixUtcDateTime();
            var entities = await _exchangeEventsRepository.GetSortedByDateRangeAsync(address, start, end, count);

            return entities.Select(Map);
        }

        private ExchangeEvent Map(IExchangeEventEntity entity)
        {
            var exchangeEvent = new ExchangeEvent();
            switch (entity.Type)
            {
                case ExchangeEventType.EthPurchase:
                    exchangeEvent.EthAmount = -entity.EthAmount;
                    exchangeEvent.TokenAmount = entity.TokenAmount;
                    break;
                case ExchangeEventType.TokenPurchase:
                    exchangeEvent.EthAmount = entity.EthAmount;
                    exchangeEvent.TokenAmount = -entity.TokenAmount;
                    break;
                case ExchangeEventType.AddLiquidity:
                    exchangeEvent.EthAmount = entity.EthAmount;
                    exchangeEvent.TokenAmount = entity.TokenAmount;
                    break;
                case ExchangeEventType.RemoveLiquidity:
                    exchangeEvent.EthAmount = -entity.EthAmount;
                    exchangeEvent.TokenAmount = -entity.TokenAmount;
                    break;
                default: 
                    throw new ArgumentException("Unknown event type", nameof(entity.Type));
            }

            exchangeEvent.Block = entity.BlockNumber;
            exchangeEvent.Event = entity.Type;
            exchangeEvent.Timestamp = entity.Timestamp.ToUnixTimestamp();
            exchangeEvent.Tx = entity.TxHash;
            exchangeEvent.User = entity.CallerAddress;
            exchangeEvent.Fee = entity.EthFee;
            
            return exchangeEvent;
        }
    }
}