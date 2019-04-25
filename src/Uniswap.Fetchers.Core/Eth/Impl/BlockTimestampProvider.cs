using System;
using System.Threading.Tasks;
using Uniswap.Common;

namespace Uniswap.Fetchers.Core.Eth.Impl
{
    public class BlockTimestampProvider : IBlockTimestampProvider
    {
        private readonly IEthGateway _ethGateway;

        public BlockTimestampProvider(IEthGateway ethGateway)
        {
            _ethGateway = ethGateway;
        }

        public async Task<DateTime> GetByBlockNumberAsync(ulong blockNumber)
        {
            var block = await _ethGateway.GetBlockWithTransactionHashesAsync(blockNumber);
            return block.Timestamp.Value.ToUnixDateTime();
        }
    }
}