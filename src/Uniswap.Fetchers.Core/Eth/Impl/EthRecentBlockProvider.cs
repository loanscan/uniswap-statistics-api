using System.Threading.Tasks;

namespace Uniswap.Fetchers.Core.Eth.Impl
{
    public class EthRecentBlockProvider : IRecentBlockProvider
    {
        private readonly IEthGateway _ethGateway;

        public EthRecentBlockProvider(IEthGateway ethGateway)
        {
            _ethGateway = ethGateway;
        }

        public async Task<ulong> GetAsync()
        {
            return (ulong) await _ethGateway.GetRecentBlockAsync();
        }
    }
}