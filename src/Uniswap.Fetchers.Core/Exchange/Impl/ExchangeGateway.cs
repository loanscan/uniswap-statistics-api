using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Uniswap.Fetchers.Core.Infra;
using Uniswap.SmartContracts.Exchange.Service;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeGateway : IExchangeGateway
    {
        private readonly IRetrier _retrier;
        private readonly ExchangeService _exchangeService;

        public ExchangeGateway(
            IRetrier retrier,
            string contractAddress,
            Func<string, ExchangeService> exchangeServiceFactory)
        {
            _retrier = retrier;
            _exchangeService = exchangeServiceFactory(contractAddress);
        }

        public async Task<string> GetTokenAddressAsync()
        {
            return await _retrier.ExecuteAsync(() => _exchangeService.TokenAddressQueryAsync());
        }

        public async Task<BigInteger> GetBalanceOfAsync(string address, ulong blockNumber)
        {
            return await _retrier.ExecuteAsync(() => _exchangeService.BalanceOfQueryAsync(address, new BlockParameter(blockNumber)));
        }
        
        public async Task<BigInteger> GetTotalSupplyAsync()
        {
            return await _retrier.ExecuteAsync(() => _exchangeService.TotalSupplyQueryAsync());
        }
    }
}