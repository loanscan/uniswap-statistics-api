using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Uniswap.Common;
using Uniswap.Data.Entities;
using Uniswap.Fetchers.Core.Eth;
using Uniswap.Fetchers.Core.Token;
using Uniswap.Fetchers.Core.Utils;
using Uniswap.SmartContracts.Exchange.CQS;

namespace Uniswap.Fetchers.Core.Exchange.Impl
{
    public class ExchangeEventMapper : IExchangeEventMapper
    {
        private const decimal NoFee = decimal.Zero;
        
        private readonly IExchangeEventEntityFactory _exchangeEventEntityFactory;
        private readonly IBlockTimestampProvider _blockTimestampProvider;
        private readonly ITokenInfoProvider _tokenInfoProvider;
        private readonly Func<string, IExchangeGateway> _exchangeGatewayFactory;

        public ExchangeEventMapper(
            IExchangeEventEntityFactory exchangeEventEntityFactory, 
            IBlockTimestampProvider blockTimestampProvider, 
            ITokenInfoProvider tokenInfoProvider,
            Func<string, IExchangeGateway> exchangeGatewayFactory)
        {
            _exchangeEventEntityFactory = exchangeEventEntityFactory;
            _blockTimestampProvider = blockTimestampProvider;
            _tokenInfoProvider = tokenInfoProvider;
            _exchangeGatewayFactory = exchangeGatewayFactory;
        }

        public Task<IExchangeEventEntity> MapAsync(IEventLog eventLog, ExchangeState exchangeState, string tokenAddress)
        {
            switch (eventLog)
            {
                case EventLog<TokenPurchaseEventDTO> log:
                    return MapToEntity(log, exchangeState, tokenAddress);
                case EventLog<EthPurchaseEventDTO> log:
                    return MapToEntity(log, exchangeState, tokenAddress);
                case EventLog<AddLiquidityEventDTO> log:
                    return MapToEntity(log, exchangeState, tokenAddress);
                case EventLog<RemoveLiquidityEventDTO> log:
                    return MapToEntity(log, exchangeState, tokenAddress);
                default:
                    throw new ArgumentException("Invalid event log type", nameof(eventLog));
            }
        }

        private async Task<IExchangeEventEntity> MapToEntity(EventLog<TokenPurchaseEventDTO> eventLog, ExchangeState exchangeState, string tokenAddress)
        {
            var (ethSold, tokensBought) =
                await ConvertEventValuesToDecimal(eventLog.Event.Eth_sold, eventLog.Event.Tokens_bought, tokenAddress);
            var ethFee = ethSold * UniswapUtils.Fee;
            
            var exchangeStateAfterEvent = new ExchangeState
            {
                EthLiquidity = exchangeState.EthLiquidity + ethSold,
                TokenLiquidity = exchangeState.TokenLiquidity - tokensBought
            };
            
            return await CreateEntityAsync(
                eventLog,
                ExchangeEventType.TokenPurchase,
                exchangeState,
                exchangeStateAfterEvent,
                eventLog.Event.Buyer,
                ethSold,
                tokensBought,
                ethFee,
                NoFee,
                tokenAddress);
        }

        private async Task<IExchangeEventEntity> MapToEntity(EventLog<EthPurchaseEventDTO> eventLog, ExchangeState exchangeState, string tokenAddress)
        {
            var (ethBought, tokensSold) =
                await ConvertEventValuesToDecimal(eventLog.Event.Eth_bought, eventLog.Event.Tokens_sold, tokenAddress);
            var tokenFee = tokensSold * UniswapUtils.Fee;
            
            var exchangeStateAfterEvent = new ExchangeState
            {
                EthLiquidity = exchangeState.EthLiquidity - ethBought,
                TokenLiquidity = exchangeState.TokenLiquidity + tokensSold
            };
            
            return await CreateEntityAsync(
                eventLog,
                ExchangeEventType.EthPurchase,
                exchangeState,
                exchangeStateAfterEvent,
                eventLog.Event.Buyer,
                ethBought,
                tokensSold,
                NoFee,
                tokenFee,
                tokenAddress);
        }

        private async Task<IExchangeEventEntity> MapToEntity(EventLog<AddLiquidityEventDTO> eventLog, ExchangeState exchangeState, string tokenAddress)
        {
            var (ethAmount, tokenAmount) =
                await ConvertEventValuesToDecimal(eventLog.Event.Eth_amount, eventLog.Event.Token_amount, tokenAddress);
            
            var exchangeStateAfterEvent = new ExchangeState
            {
                EthLiquidity = exchangeState.EthLiquidity + ethAmount,
                TokenLiquidity = exchangeState.TokenLiquidity + tokenAmount
            };

            return await CreateEntityAsync(
                eventLog,
                ExchangeEventType.AddLiquidity,
                exchangeState,
                exchangeStateAfterEvent,
                eventLog.Event.Provider,
                ethAmount,
                tokenAmount,
                NoFee,
                NoFee,
                tokenAddress);
        }

        private async Task<IExchangeEventEntity> MapToEntity(EventLog<RemoveLiquidityEventDTO> eventLog, ExchangeState exchangeState, string tokenAddress)
        {
            var (ethAmount, tokenAmount) =
                await ConvertEventValuesToDecimal(eventLog.Event.Eth_amount, eventLog.Event.Token_amount, tokenAddress);
            
            var exchangeStateAfterEvent = new ExchangeState
            {
                EthLiquidity = exchangeState.EthLiquidity - ethAmount,
                TokenLiquidity = exchangeState.TokenLiquidity - tokenAmount
            };

            return await CreateEntityAsync(
                eventLog,
                ExchangeEventType.RemoveLiquidity,
                exchangeState,
                exchangeStateAfterEvent,
                eventLog.Event.Provider,
                ethAmount,
                tokenAmount,
                NoFee,
                NoFee,
                tokenAddress);
        }

        private async Task<IExchangeEventEntity> CreateEntityAsync(
            IEventLog eventLog,
            ExchangeEventType eventType,
            ExchangeState beforeState,
            ExchangeState afterState,
            string callerAddress,
            decimal ethAmount,
            decimal tokenAmount,
            decimal ethFee,
            decimal tokenFee,
            string tokenAddress)
        {
            var blockNumber = (ulong) eventLog.Log.BlockNumber.Value;
            var blockTimestamp =
                await _blockTimestampProvider.GetByBlockNumberAsync(blockNumber);

            var exchangeAddress = eventLog.Log.Address;

            var callerBalance = await _exchangeGatewayFactory(exchangeAddress).GetBalanceOfAsync(callerAddress, blockNumber);
            var tokenInfo = await _tokenInfoProvider.GetAsync(tokenAddress);
            
            return _exchangeEventEntityFactory.Create(
                $"{eventLog.Log.TransactionHash}_{eventLog.Log.LogIndex.Value}",
                exchangeAddress,
                callerAddress,
                eventType, 
                ethAmount,
                tokenAmount,
                beforeState.EthLiquidity,
                afterState.EthLiquidity,
                beforeState.TokenLiquidity,
                afterState.TokenLiquidity,
                eventLog.Log.TransactionHash,
                (int)eventLog.Log.LogIndex.Value,
                blockTimestamp,
                ethFee,
                tokenFee,
                tokenAddress,
                blockNumber,
                callerBalance.ToDecimal(tokenInfo.Decimals));
        }

        private async Task<(decimal, decimal)> ConvertEventValuesToDecimal(BigInteger ethAmount, BigInteger tokenAmount, string tokenAddress)
        {
            var tokenInfo = await _tokenInfoProvider.GetAsync(tokenAddress);
            return (ethAmount.ToDecimal(EthUtils.Decimals), tokenAmount.ToDecimal(tokenInfo.Decimals));
        }
    }
}