using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Uniswap.Data.Entities;
using Uniswap.Fetchers.Core.Eth;
using Uniswap.Fetchers.Core.Exchange;
using Uniswap.Fetchers.Core.Token;
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.Fetchers.Core.Factory.Impl
{
    public class ExchangeMapper : IExchangeMapper
    {
        private readonly ITokenInfoProvider _tokenInfoProvider;
        private readonly ITokenLiquidityProvider _tokenLiquidityProvider;
        private readonly IEthLiquidityProvider _ethLiquidityProvider;
        private readonly IExchangeTotalSupplyProvider _exchangeTotalSupplyProvider;
        private readonly IExchangeThemeProvider _exchangeThemeProvider;
        private readonly IExchangeEntityFactory _exchangeEntityFactory;

        public ExchangeMapper(
            ITokenInfoProvider tokenInfoProvider,
            ITokenLiquidityProvider tokenLiquidityProvider,
            IEthLiquidityProvider ethLiquidityProvider,
            IExchangeTotalSupplyProvider exchangeTotalSupplyProvider,
            IExchangeThemeProvider exchangeThemeProvider,
            IExchangeEntityFactory exchangeEntityFactory)
        {
            _tokenInfoProvider = tokenInfoProvider;
            _tokenLiquidityProvider = tokenLiquidityProvider;
            _ethLiquidityProvider = ethLiquidityProvider;
            _exchangeTotalSupplyProvider = exchangeTotalSupplyProvider;
            _exchangeThemeProvider = exchangeThemeProvider;
            _exchangeEntityFactory = exchangeEntityFactory;
        }
        
        public async Task<IExchangeEntity> MapAsync(EventLog<NewExchangeEventDTO> eventLog)
        {
            var exchangeAddress = eventLog.Event.Exchange;
            var tokenAddress = eventLog.Event.Token;
            var tokenInfo = await _tokenInfoProvider.GetAsync(tokenAddress);
            var blockNumber = (ulong) (BigInteger) eventLog.Log.BlockNumber;
            var tokenLiquidity = await _tokenLiquidityProvider.GetAsync(tokenAddress, exchangeAddress);
            var ethLiquidity = await _ethLiquidityProvider.GetAsync(exchangeAddress);
            var totalSupply = await _exchangeTotalSupplyProvider.GetAsync(exchangeAddress);
            var theme = await _exchangeThemeProvider.GetAsync(tokenInfo.Symbol);
            
            return _exchangeEntityFactory.Create(exchangeAddress, tokenAddress, tokenInfo, blockNumber, ethLiquidity, tokenLiquidity, totalSupply, theme);
        }
    }
}