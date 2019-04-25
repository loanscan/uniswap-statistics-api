using System.Threading.Tasks;
using Uniswap.Fetchers.Core.Utils;

namespace Uniswap.Fetchers.Core.Eth.Impl
{
    public class EthLiquidityProvider : IEthLiquidityProvider
    {
        private const int EthDecimals = 18;
        
        private readonly IEthGateway _ethGateway;

        public EthLiquidityProvider(IEthGateway ethGateway)
        {
            _ethGateway = ethGateway;
        }

        public async Task<decimal> GetAsync(string address)
        {
            var balance = await _ethGateway.GetBalanceAsync(address);

            return balance.ToDecimal(EthDecimals);
        }
        
        public async Task<decimal> GetAsync(string address, ulong blockNumber)
        {
            var balance = await _ethGateway.GetBalanceAsync(address, blockNumber);

            return balance.ToDecimal(EthDecimals);
        }
    }
}