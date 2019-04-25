using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Uniswap.SmartContracts.Exchange.CQS;

namespace Uniswap.SmartContracts.Exchange.Service
{
    public partial class ExchangeService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ExchangeDeployment exchangeDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ExchangeDeployment>().SendRequestAndWaitForReceiptAsync(exchangeDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ExchangeDeployment exchangeDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ExchangeDeployment>().SendRequestAsync(exchangeDeployment);
        }

        public static async Task<ExchangeService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ExchangeDeployment exchangeDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, exchangeDeployment, cancellationTokenSource);
            return new ExchangeService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ExchangeService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> SetupRequestAsync(SetupFunction setupFunction)
        {
             return ContractHandler.SendRequestAsync(setupFunction);
        }

        public Task<TransactionReceipt> SetupRequestAndWaitForReceiptAsync(SetupFunction setupFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setupFunction, cancellationToken);
        }

        public Task<string> SetupRequestAsync(string token_addr)
        {
            var setupFunction = new SetupFunction();
                setupFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAsync(setupFunction);
        }

        public Task<TransactionReceipt> SetupRequestAndWaitForReceiptAsync(string token_addr, CancellationTokenSource cancellationToken = null)
        {
            var setupFunction = new SetupFunction();
                setupFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setupFunction, cancellationToken);
        }

        public Task<string> AddLiquidityRequestAsync(AddLiquidityFunction addLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(addLiquidityFunction);
        }

        public Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(AddLiquidityFunction addLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }

        public Task<string> AddLiquidityRequestAsync(BigInteger min_liquidity, BigInteger max_tokens, BigInteger deadline)
        {
            var addLiquidityFunction = new AddLiquidityFunction();
                addLiquidityFunction.Min_liquidity = min_liquidity;
                addLiquidityFunction.Max_tokens = max_tokens;
                addLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(addLiquidityFunction);
        }

        public Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(BigInteger min_liquidity, BigInteger max_tokens, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var addLiquidityFunction = new AddLiquidityFunction();
                addLiquidityFunction.Min_liquidity = min_liquidity;
                addLiquidityFunction.Max_tokens = max_tokens;
                addLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }

        public Task<string> RemoveLiquidityRequestAsync(RemoveLiquidityFunction removeLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(removeLiquidityFunction);
        }

        public Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(RemoveLiquidityFunction removeLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeLiquidityFunction, cancellationToken);
        }

        public Task<string> RemoveLiquidityRequestAsync(BigInteger amount, BigInteger min_eth, BigInteger min_tokens, BigInteger deadline)
        {
            var removeLiquidityFunction = new RemoveLiquidityFunction();
                removeLiquidityFunction.Amount = amount;
                removeLiquidityFunction.Min_eth = min_eth;
                removeLiquidityFunction.Min_tokens = min_tokens;
                removeLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(removeLiquidityFunction);
        }

        public Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(BigInteger amount, BigInteger min_eth, BigInteger min_tokens, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var removeLiquidityFunction = new RemoveLiquidityFunction();
                removeLiquidityFunction.Amount = amount;
                removeLiquidityFunction.Min_eth = min_eth;
                removeLiquidityFunction.Min_tokens = min_tokens;
                removeLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeLiquidityFunction, cancellationToken);
        }

        public Task<string> Default__RequestAsync(Default__Function default__Function)
        {
             return ContractHandler.SendRequestAsync(default__Function);
        }

        public Task<string> Default__RequestAsync()
        {
             return ContractHandler.SendRequestAsync<Default__Function>();
        }

        public Task<TransactionReceipt> Default__RequestAndWaitForReceiptAsync(Default__Function default__Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(default__Function, cancellationToken);
        }

        public Task<TransactionReceipt> Default__RequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<Default__Function>(null, cancellationToken);
        }

        public Task<string> EthToTokenSwapInputRequestAsync(EthToTokenSwapInputFunction ethToTokenSwapInputFunction)
        {
             return ContractHandler.SendRequestAsync(ethToTokenSwapInputFunction);
        }

        public Task<TransactionReceipt> EthToTokenSwapInputRequestAndWaitForReceiptAsync(EthToTokenSwapInputFunction ethToTokenSwapInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenSwapInputFunction, cancellationToken);
        }

        public Task<string> EthToTokenSwapInputRequestAsync(BigInteger min_tokens, BigInteger deadline)
        {
            var ethToTokenSwapInputFunction = new EthToTokenSwapInputFunction();
                ethToTokenSwapInputFunction.Min_tokens = min_tokens;
                ethToTokenSwapInputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(ethToTokenSwapInputFunction);
        }

        public Task<TransactionReceipt> EthToTokenSwapInputRequestAndWaitForReceiptAsync(BigInteger min_tokens, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var ethToTokenSwapInputFunction = new EthToTokenSwapInputFunction();
                ethToTokenSwapInputFunction.Min_tokens = min_tokens;
                ethToTokenSwapInputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenSwapInputFunction, cancellationToken);
        }

        public Task<string> EthToTokenTransferInputRequestAsync(EthToTokenTransferInputFunction ethToTokenTransferInputFunction)
        {
             return ContractHandler.SendRequestAsync(ethToTokenTransferInputFunction);
        }

        public Task<TransactionReceipt> EthToTokenTransferInputRequestAndWaitForReceiptAsync(EthToTokenTransferInputFunction ethToTokenTransferInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenTransferInputFunction, cancellationToken);
        }

        public Task<string> EthToTokenTransferInputRequestAsync(BigInteger min_tokens, BigInteger deadline, string recipient)
        {
            var ethToTokenTransferInputFunction = new EthToTokenTransferInputFunction();
                ethToTokenTransferInputFunction.Min_tokens = min_tokens;
                ethToTokenTransferInputFunction.Deadline = deadline;
                ethToTokenTransferInputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(ethToTokenTransferInputFunction);
        }

        public Task<TransactionReceipt> EthToTokenTransferInputRequestAndWaitForReceiptAsync(BigInteger min_tokens, BigInteger deadline, string recipient, CancellationTokenSource cancellationToken = null)
        {
            var ethToTokenTransferInputFunction = new EthToTokenTransferInputFunction();
                ethToTokenTransferInputFunction.Min_tokens = min_tokens;
                ethToTokenTransferInputFunction.Deadline = deadline;
                ethToTokenTransferInputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenTransferInputFunction, cancellationToken);
        }

        public Task<string> EthToTokenSwapOutputRequestAsync(EthToTokenSwapOutputFunction ethToTokenSwapOutputFunction)
        {
             return ContractHandler.SendRequestAsync(ethToTokenSwapOutputFunction);
        }

        public Task<TransactionReceipt> EthToTokenSwapOutputRequestAndWaitForReceiptAsync(EthToTokenSwapOutputFunction ethToTokenSwapOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenSwapOutputFunction, cancellationToken);
        }

        public Task<string> EthToTokenSwapOutputRequestAsync(BigInteger tokens_bought, BigInteger deadline)
        {
            var ethToTokenSwapOutputFunction = new EthToTokenSwapOutputFunction();
                ethToTokenSwapOutputFunction.Tokens_bought = tokens_bought;
                ethToTokenSwapOutputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(ethToTokenSwapOutputFunction);
        }

        public Task<TransactionReceipt> EthToTokenSwapOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var ethToTokenSwapOutputFunction = new EthToTokenSwapOutputFunction();
                ethToTokenSwapOutputFunction.Tokens_bought = tokens_bought;
                ethToTokenSwapOutputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenSwapOutputFunction, cancellationToken);
        }

        public Task<string> EthToTokenTransferOutputRequestAsync(EthToTokenTransferOutputFunction ethToTokenTransferOutputFunction)
        {
             return ContractHandler.SendRequestAsync(ethToTokenTransferOutputFunction);
        }

        public Task<TransactionReceipt> EthToTokenTransferOutputRequestAndWaitForReceiptAsync(EthToTokenTransferOutputFunction ethToTokenTransferOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenTransferOutputFunction, cancellationToken);
        }

        public Task<string> EthToTokenTransferOutputRequestAsync(BigInteger tokens_bought, BigInteger deadline, string recipient)
        {
            var ethToTokenTransferOutputFunction = new EthToTokenTransferOutputFunction();
                ethToTokenTransferOutputFunction.Tokens_bought = tokens_bought;
                ethToTokenTransferOutputFunction.Deadline = deadline;
                ethToTokenTransferOutputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(ethToTokenTransferOutputFunction);
        }

        public Task<TransactionReceipt> EthToTokenTransferOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger deadline, string recipient, CancellationTokenSource cancellationToken = null)
        {
            var ethToTokenTransferOutputFunction = new EthToTokenTransferOutputFunction();
                ethToTokenTransferOutputFunction.Tokens_bought = tokens_bought;
                ethToTokenTransferOutputFunction.Deadline = deadline;
                ethToTokenTransferOutputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(ethToTokenTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToEthSwapInputRequestAsync(TokenToEthSwapInputFunction tokenToEthSwapInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToEthSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToEthSwapInputRequestAndWaitForReceiptAsync(TokenToEthSwapInputFunction tokenToEthSwapInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToEthSwapInputRequestAsync(BigInteger tokens_sold, BigInteger min_eth, BigInteger deadline)
        {
            var tokenToEthSwapInputFunction = new TokenToEthSwapInputFunction();
                tokenToEthSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToEthSwapInputFunction.Min_eth = min_eth;
                tokenToEthSwapInputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(tokenToEthSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToEthSwapInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_eth, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var tokenToEthSwapInputFunction = new TokenToEthSwapInputFunction();
                tokenToEthSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToEthSwapInputFunction.Min_eth = min_eth;
                tokenToEthSwapInputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToEthTransferInputRequestAsync(TokenToEthTransferInputFunction tokenToEthTransferInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToEthTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToEthTransferInputRequestAndWaitForReceiptAsync(TokenToEthTransferInputFunction tokenToEthTransferInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToEthTransferInputRequestAsync(BigInteger tokens_sold, BigInteger min_eth, BigInteger deadline, string recipient)
        {
            var tokenToEthTransferInputFunction = new TokenToEthTransferInputFunction();
                tokenToEthTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToEthTransferInputFunction.Min_eth = min_eth;
                tokenToEthTransferInputFunction.Deadline = deadline;
                tokenToEthTransferInputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(tokenToEthTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToEthTransferInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_eth, BigInteger deadline, string recipient, CancellationTokenSource cancellationToken = null)
        {
            var tokenToEthTransferInputFunction = new TokenToEthTransferInputFunction();
                tokenToEthTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToEthTransferInputFunction.Min_eth = min_eth;
                tokenToEthTransferInputFunction.Deadline = deadline;
                tokenToEthTransferInputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToEthSwapOutputRequestAsync(TokenToEthSwapOutputFunction tokenToEthSwapOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToEthSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToEthSwapOutputRequestAndWaitForReceiptAsync(TokenToEthSwapOutputFunction tokenToEthSwapOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToEthSwapOutputRequestAsync(BigInteger eth_bought, BigInteger max_tokens, BigInteger deadline)
        {
            var tokenToEthSwapOutputFunction = new TokenToEthSwapOutputFunction();
                tokenToEthSwapOutputFunction.Eth_bought = eth_bought;
                tokenToEthSwapOutputFunction.Max_tokens = max_tokens;
                tokenToEthSwapOutputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(tokenToEthSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToEthSwapOutputRequestAndWaitForReceiptAsync(BigInteger eth_bought, BigInteger max_tokens, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var tokenToEthSwapOutputFunction = new TokenToEthSwapOutputFunction();
                tokenToEthSwapOutputFunction.Eth_bought = eth_bought;
                tokenToEthSwapOutputFunction.Max_tokens = max_tokens;
                tokenToEthSwapOutputFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToEthTransferOutputRequestAsync(TokenToEthTransferOutputFunction tokenToEthTransferOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToEthTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToEthTransferOutputRequestAndWaitForReceiptAsync(TokenToEthTransferOutputFunction tokenToEthTransferOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToEthTransferOutputRequestAsync(BigInteger eth_bought, BigInteger max_tokens, BigInteger deadline, string recipient)
        {
            var tokenToEthTransferOutputFunction = new TokenToEthTransferOutputFunction();
                tokenToEthTransferOutputFunction.Eth_bought = eth_bought;
                tokenToEthTransferOutputFunction.Max_tokens = max_tokens;
                tokenToEthTransferOutputFunction.Deadline = deadline;
                tokenToEthTransferOutputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAsync(tokenToEthTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToEthTransferOutputRequestAndWaitForReceiptAsync(BigInteger eth_bought, BigInteger max_tokens, BigInteger deadline, string recipient, CancellationTokenSource cancellationToken = null)
        {
            var tokenToEthTransferOutputFunction = new TokenToEthTransferOutputFunction();
                tokenToEthTransferOutputFunction.Eth_bought = eth_bought;
                tokenToEthTransferOutputFunction.Max_tokens = max_tokens;
                tokenToEthTransferOutputFunction.Deadline = deadline;
                tokenToEthTransferOutputFunction.Recipient = recipient;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToEthTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenSwapInputRequestAsync(TokenToTokenSwapInputFunction tokenToTokenSwapInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToTokenSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenSwapInputRequestAndWaitForReceiptAsync(TokenToTokenSwapInputFunction tokenToTokenSwapInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenSwapInputRequestAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string token_addr)
        {
            var tokenToTokenSwapInputFunction = new TokenToTokenSwapInputFunction();
                tokenToTokenSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToTokenSwapInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToTokenSwapInputFunction.Min_eth_bought = min_eth_bought;
                tokenToTokenSwapInputFunction.Deadline = deadline;
                tokenToTokenSwapInputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAsync(tokenToTokenSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenSwapInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string token_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToTokenSwapInputFunction = new TokenToTokenSwapInputFunction();
                tokenToTokenSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToTokenSwapInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToTokenSwapInputFunction.Min_eth_bought = min_eth_bought;
                tokenToTokenSwapInputFunction.Deadline = deadline;
                tokenToTokenSwapInputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenTransferInputRequestAsync(TokenToTokenTransferInputFunction tokenToTokenTransferInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToTokenTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenTransferInputRequestAndWaitForReceiptAsync(TokenToTokenTransferInputFunction tokenToTokenTransferInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenTransferInputRequestAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string recipient, string token_addr)
        {
            var tokenToTokenTransferInputFunction = new TokenToTokenTransferInputFunction();
                tokenToTokenTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToTokenTransferInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToTokenTransferInputFunction.Min_eth_bought = min_eth_bought;
                tokenToTokenTransferInputFunction.Deadline = deadline;
                tokenToTokenTransferInputFunction.Recipient = recipient;
                tokenToTokenTransferInputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAsync(tokenToTokenTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenTransferInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string recipient, string token_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToTokenTransferInputFunction = new TokenToTokenTransferInputFunction();
                tokenToTokenTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToTokenTransferInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToTokenTransferInputFunction.Min_eth_bought = min_eth_bought;
                tokenToTokenTransferInputFunction.Deadline = deadline;
                tokenToTokenTransferInputFunction.Recipient = recipient;
                tokenToTokenTransferInputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenSwapOutputRequestAsync(TokenToTokenSwapOutputFunction tokenToTokenSwapOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToTokenSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenSwapOutputRequestAndWaitForReceiptAsync(TokenToTokenSwapOutputFunction tokenToTokenSwapOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenSwapOutputRequestAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string token_addr)
        {
            var tokenToTokenSwapOutputFunction = new TokenToTokenSwapOutputFunction();
                tokenToTokenSwapOutputFunction.Tokens_bought = tokens_bought;
                tokenToTokenSwapOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToTokenSwapOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToTokenSwapOutputFunction.Deadline = deadline;
                tokenToTokenSwapOutputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAsync(tokenToTokenSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenSwapOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string token_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToTokenSwapOutputFunction = new TokenToTokenSwapOutputFunction();
                tokenToTokenSwapOutputFunction.Tokens_bought = tokens_bought;
                tokenToTokenSwapOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToTokenSwapOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToTokenSwapOutputFunction.Deadline = deadline;
                tokenToTokenSwapOutputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenTransferOutputRequestAsync(TokenToTokenTransferOutputFunction tokenToTokenTransferOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToTokenTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenTransferOutputRequestAndWaitForReceiptAsync(TokenToTokenTransferOutputFunction tokenToTokenTransferOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToTokenTransferOutputRequestAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string recipient, string token_addr)
        {
            var tokenToTokenTransferOutputFunction = new TokenToTokenTransferOutputFunction();
                tokenToTokenTransferOutputFunction.Tokens_bought = tokens_bought;
                tokenToTokenTransferOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToTokenTransferOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToTokenTransferOutputFunction.Deadline = deadline;
                tokenToTokenTransferOutputFunction.Recipient = recipient;
                tokenToTokenTransferOutputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAsync(tokenToTokenTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToTokenTransferOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string recipient, string token_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToTokenTransferOutputFunction = new TokenToTokenTransferOutputFunction();
                tokenToTokenTransferOutputFunction.Tokens_bought = tokens_bought;
                tokenToTokenTransferOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToTokenTransferOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToTokenTransferOutputFunction.Deadline = deadline;
                tokenToTokenTransferOutputFunction.Recipient = recipient;
                tokenToTokenTransferOutputFunction.Token_addr = token_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToTokenTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeSwapInputRequestAsync(TokenToExchangeSwapInputFunction tokenToExchangeSwapInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToExchangeSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeSwapInputRequestAndWaitForReceiptAsync(TokenToExchangeSwapInputFunction tokenToExchangeSwapInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeSwapInputRequestAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string exchange_addr)
        {
            var tokenToExchangeSwapInputFunction = new TokenToExchangeSwapInputFunction();
                tokenToExchangeSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToExchangeSwapInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToExchangeSwapInputFunction.Min_eth_bought = min_eth_bought;
                tokenToExchangeSwapInputFunction.Deadline = deadline;
                tokenToExchangeSwapInputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAsync(tokenToExchangeSwapInputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeSwapInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string exchange_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToExchangeSwapInputFunction = new TokenToExchangeSwapInputFunction();
                tokenToExchangeSwapInputFunction.Tokens_sold = tokens_sold;
                tokenToExchangeSwapInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToExchangeSwapInputFunction.Min_eth_bought = min_eth_bought;
                tokenToExchangeSwapInputFunction.Deadline = deadline;
                tokenToExchangeSwapInputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeSwapInputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeTransferInputRequestAsync(TokenToExchangeTransferInputFunction tokenToExchangeTransferInputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToExchangeTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeTransferInputRequestAndWaitForReceiptAsync(TokenToExchangeTransferInputFunction tokenToExchangeTransferInputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeTransferInputRequestAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string recipient, string exchange_addr)
        {
            var tokenToExchangeTransferInputFunction = new TokenToExchangeTransferInputFunction();
                tokenToExchangeTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToExchangeTransferInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToExchangeTransferInputFunction.Min_eth_bought = min_eth_bought;
                tokenToExchangeTransferInputFunction.Deadline = deadline;
                tokenToExchangeTransferInputFunction.Recipient = recipient;
                tokenToExchangeTransferInputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAsync(tokenToExchangeTransferInputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeTransferInputRequestAndWaitForReceiptAsync(BigInteger tokens_sold, BigInteger min_tokens_bought, BigInteger min_eth_bought, BigInteger deadline, string recipient, string exchange_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToExchangeTransferInputFunction = new TokenToExchangeTransferInputFunction();
                tokenToExchangeTransferInputFunction.Tokens_sold = tokens_sold;
                tokenToExchangeTransferInputFunction.Min_tokens_bought = min_tokens_bought;
                tokenToExchangeTransferInputFunction.Min_eth_bought = min_eth_bought;
                tokenToExchangeTransferInputFunction.Deadline = deadline;
                tokenToExchangeTransferInputFunction.Recipient = recipient;
                tokenToExchangeTransferInputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeTransferInputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeSwapOutputRequestAsync(TokenToExchangeSwapOutputFunction tokenToExchangeSwapOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToExchangeSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeSwapOutputRequestAndWaitForReceiptAsync(TokenToExchangeSwapOutputFunction tokenToExchangeSwapOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeSwapOutputRequestAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string exchange_addr)
        {
            var tokenToExchangeSwapOutputFunction = new TokenToExchangeSwapOutputFunction();
                tokenToExchangeSwapOutputFunction.Tokens_bought = tokens_bought;
                tokenToExchangeSwapOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToExchangeSwapOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToExchangeSwapOutputFunction.Deadline = deadline;
                tokenToExchangeSwapOutputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAsync(tokenToExchangeSwapOutputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeSwapOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string exchange_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToExchangeSwapOutputFunction = new TokenToExchangeSwapOutputFunction();
                tokenToExchangeSwapOutputFunction.Tokens_bought = tokens_bought;
                tokenToExchangeSwapOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToExchangeSwapOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToExchangeSwapOutputFunction.Deadline = deadline;
                tokenToExchangeSwapOutputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeSwapOutputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeTransferOutputRequestAsync(TokenToExchangeTransferOutputFunction tokenToExchangeTransferOutputFunction)
        {
             return ContractHandler.SendRequestAsync(tokenToExchangeTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeTransferOutputRequestAndWaitForReceiptAsync(TokenToExchangeTransferOutputFunction tokenToExchangeTransferOutputFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeTransferOutputFunction, cancellationToken);
        }

        public Task<string> TokenToExchangeTransferOutputRequestAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string recipient, string exchange_addr)
        {
            var tokenToExchangeTransferOutputFunction = new TokenToExchangeTransferOutputFunction();
                tokenToExchangeTransferOutputFunction.Tokens_bought = tokens_bought;
                tokenToExchangeTransferOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToExchangeTransferOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToExchangeTransferOutputFunction.Deadline = deadline;
                tokenToExchangeTransferOutputFunction.Recipient = recipient;
                tokenToExchangeTransferOutputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAsync(tokenToExchangeTransferOutputFunction);
        }

        public Task<TransactionReceipt> TokenToExchangeTransferOutputRequestAndWaitForReceiptAsync(BigInteger tokens_bought, BigInteger max_tokens_sold, BigInteger max_eth_sold, BigInteger deadline, string recipient, string exchange_addr, CancellationTokenSource cancellationToken = null)
        {
            var tokenToExchangeTransferOutputFunction = new TokenToExchangeTransferOutputFunction();
                tokenToExchangeTransferOutputFunction.Tokens_bought = tokens_bought;
                tokenToExchangeTransferOutputFunction.Max_tokens_sold = max_tokens_sold;
                tokenToExchangeTransferOutputFunction.Max_eth_sold = max_eth_sold;
                tokenToExchangeTransferOutputFunction.Deadline = deadline;
                tokenToExchangeTransferOutputFunction.Recipient = recipient;
                tokenToExchangeTransferOutputFunction.Exchange_addr = exchange_addr;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tokenToExchangeTransferOutputFunction, cancellationToken);
        }

        public Task<BigInteger> GetEthToTokenInputPriceQueryAsync(GetEthToTokenInputPriceFunction getEthToTokenInputPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEthToTokenInputPriceFunction, BigInteger>(getEthToTokenInputPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetEthToTokenInputPriceQueryAsync(BigInteger eth_sold, BlockParameter blockParameter = null)
        {
            var getEthToTokenInputPriceFunction = new GetEthToTokenInputPriceFunction();
                getEthToTokenInputPriceFunction.Eth_sold = eth_sold;
            
            return ContractHandler.QueryAsync<GetEthToTokenInputPriceFunction, BigInteger>(getEthToTokenInputPriceFunction, blockParameter);
        }

        public Task<BigInteger> GetEthToTokenOutputPriceQueryAsync(GetEthToTokenOutputPriceFunction getEthToTokenOutputPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEthToTokenOutputPriceFunction, BigInteger>(getEthToTokenOutputPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetEthToTokenOutputPriceQueryAsync(BigInteger tokens_bought, BlockParameter blockParameter = null)
        {
            var getEthToTokenOutputPriceFunction = new GetEthToTokenOutputPriceFunction();
                getEthToTokenOutputPriceFunction.Tokens_bought = tokens_bought;
            
            return ContractHandler.QueryAsync<GetEthToTokenOutputPriceFunction, BigInteger>(getEthToTokenOutputPriceFunction, blockParameter);
        }

        public Task<BigInteger> GetTokenToEthInputPriceQueryAsync(GetTokenToEthInputPriceFunction getTokenToEthInputPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenToEthInputPriceFunction, BigInteger>(getTokenToEthInputPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTokenToEthInputPriceQueryAsync(BigInteger tokens_sold, BlockParameter blockParameter = null)
        {
            var getTokenToEthInputPriceFunction = new GetTokenToEthInputPriceFunction();
                getTokenToEthInputPriceFunction.Tokens_sold = tokens_sold;
            
            return ContractHandler.QueryAsync<GetTokenToEthInputPriceFunction, BigInteger>(getTokenToEthInputPriceFunction, blockParameter);
        }

        public Task<BigInteger> GetTokenToEthOutputPriceQueryAsync(GetTokenToEthOutputPriceFunction getTokenToEthOutputPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenToEthOutputPriceFunction, BigInteger>(getTokenToEthOutputPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTokenToEthOutputPriceQueryAsync(BigInteger eth_bought, BlockParameter blockParameter = null)
        {
            var getTokenToEthOutputPriceFunction = new GetTokenToEthOutputPriceFunction();
                getTokenToEthOutputPriceFunction.Eth_bought = eth_bought;
            
            return ContractHandler.QueryAsync<GetTokenToEthOutputPriceFunction, BigInteger>(getTokenToEthOutputPriceFunction, blockParameter);
        }

        public Task<string> TokenAddressQueryAsync(TokenAddressFunction tokenAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenAddressFunction, string>(tokenAddressFunction, blockParameter);
        }

        
        public Task<string> TokenAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenAddressFunction, string>(null, blockParameter);
        }

        public Task<string> FactoryAddressQueryAsync(FactoryAddressFunction factoryAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryAddressFunction, string>(factoryAddressFunction, blockParameter);
        }

        
        public Task<string> FactoryAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryAddressFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        
        public Task<BigInteger> BalanceOfQueryAsync(string owner, BlockParameter blockParameter = null)
        {
            var balanceOfFunction = new BalanceOfFunction();
                balanceOfFunction.Owner = owner;
            
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        public Task<string> TransferRequestAsync(TransferFunction transferFunction)
        {
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferRequestAsync(string to, BigInteger value)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
        {
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(string from, string to, BigInteger value)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
        {
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<string> ApproveRequestAsync(string spender, BigInteger value)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        
        public Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null)
        {
            var allowanceFunction = new AllowanceFunction();
                allowanceFunction.Owner = owner;
                allowanceFunction.Spender = spender;
            
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        public Task<byte[]> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, byte[]>(nameFunction, blockParameter);
        }

        
        public Task<byte[]> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, byte[]>(symbolFunction, blockParameter);
        }

        
        public Task<byte[]> SymbolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, byte[]>(null, blockParameter);
        }

        public Task<BigInteger> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, BigInteger>(decimalsFunction, blockParameter);
        }

        
        public Task<BigInteger> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }
    }
}
