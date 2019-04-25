using System.Threading.Tasks;
using Nethereum.Contracts;
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.Fetchers.Core.Factory
{
    public interface IExchangeProcessor
    {
        Task ProcessAsync(EventLog<NewExchangeEventDTO> eventLog);
    }
}