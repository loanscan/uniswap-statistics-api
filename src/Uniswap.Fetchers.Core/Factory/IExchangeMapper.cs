using System.Threading.Tasks;
using Nethereum.Contracts;
using Uniswap.Data.Entities;
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.Fetchers.Core.Factory
{
    public interface IExchangeMapper
    {
        Task<IExchangeEntity> MapAsync(EventLog<NewExchangeEventDTO> eventLog);
    }
}