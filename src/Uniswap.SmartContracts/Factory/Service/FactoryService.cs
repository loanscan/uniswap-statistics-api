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
using Uniswap.SmartContracts.Factory.CQS;

namespace Uniswap.SmartContracts.Factory.Service
{
    public partial class FactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FactoryDeployment factoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FactoryDeployment>().SendRequestAndWaitForReceiptAsync(factoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FactoryDeployment factoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FactoryDeployment>().SendRequestAsync(factoryDeployment);
        }

        public static async Task<FactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FactoryDeployment factoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, factoryDeployment, cancellationTokenSource);
            return new FactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> InitializeFactoryRequestAsync(InitializeFactoryFunction initializeFactoryFunction)
        {
             return ContractHandler.SendRequestAsync(initializeFactoryFunction);
        }

        public Task<TransactionReceipt> InitializeFactoryRequestAndWaitForReceiptAsync(InitializeFactoryFunction initializeFactoryFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFactoryFunction, cancellationToken);
        }

        public Task<string> InitializeFactoryRequestAsync(string template)
        {
            var initializeFactoryFunction = new InitializeFactoryFunction();
                initializeFactoryFunction.Template = template;
            
             return ContractHandler.SendRequestAsync(initializeFactoryFunction);
        }

        public Task<TransactionReceipt> InitializeFactoryRequestAndWaitForReceiptAsync(string template, CancellationTokenSource cancellationToken = null)
        {
            var initializeFactoryFunction = new InitializeFactoryFunction();
                initializeFactoryFunction.Template = template;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFactoryFunction, cancellationToken);
        }

        public Task<string> CreateExchangeRequestAsync(CreateExchangeFunction createExchangeFunction)
        {
             return ContractHandler.SendRequestAsync(createExchangeFunction);
        }

        public Task<TransactionReceipt> CreateExchangeRequestAndWaitForReceiptAsync(CreateExchangeFunction createExchangeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createExchangeFunction, cancellationToken);
        }

        public Task<string> CreateExchangeRequestAsync(string token)
        {
            var createExchangeFunction = new CreateExchangeFunction();
                createExchangeFunction.Token = token;
            
             return ContractHandler.SendRequestAsync(createExchangeFunction);
        }

        public Task<TransactionReceipt> CreateExchangeRequestAndWaitForReceiptAsync(string token, CancellationTokenSource cancellationToken = null)
        {
            var createExchangeFunction = new CreateExchangeFunction();
                createExchangeFunction.Token = token;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createExchangeFunction, cancellationToken);
        }

        public Task<string> GetExchangeQueryAsync(GetExchangeFunction getExchangeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetExchangeFunction, string>(getExchangeFunction, blockParameter);
        }

        
        public Task<string> GetExchangeQueryAsync(string token, BlockParameter blockParameter = null)
        {
            var getExchangeFunction = new GetExchangeFunction();
                getExchangeFunction.Token = token;
            
            return ContractHandler.QueryAsync<GetExchangeFunction, string>(getExchangeFunction, blockParameter);
        }

        public Task<string> GetTokenQueryAsync(GetTokenFunction getTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenFunction, string>(getTokenFunction, blockParameter);
        }

        
        public Task<string> GetTokenQueryAsync(string exchange, BlockParameter blockParameter = null)
        {
            var getTokenFunction = new GetTokenFunction();
                getTokenFunction.Exchange = exchange;
            
            return ContractHandler.QueryAsync<GetTokenFunction, string>(getTokenFunction, blockParameter);
        }

        public Task<string> GetTokenWithIdQueryAsync(GetTokenWithIdFunction getTokenWithIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenWithIdFunction, string>(getTokenWithIdFunction, blockParameter);
        }

        
        public Task<string> GetTokenWithIdQueryAsync(BigInteger token_id, BlockParameter blockParameter = null)
        {
            var getTokenWithIdFunction = new GetTokenWithIdFunction();
                getTokenWithIdFunction.Token_id = token_id;
            
            return ContractHandler.QueryAsync<GetTokenWithIdFunction, string>(getTokenWithIdFunction, blockParameter);
        }

        public Task<string> ExchangeTemplateQueryAsync(ExchangeTemplateFunction exchangeTemplateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ExchangeTemplateFunction, string>(exchangeTemplateFunction, blockParameter);
        }

        
        public Task<string> ExchangeTemplateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ExchangeTemplateFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TokenCountQueryAsync(TokenCountFunction tokenCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenCountFunction, BigInteger>(tokenCountFunction, blockParameter);
        }

        
        public Task<BigInteger> TokenCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenCountFunction, BigInteger>(null, blockParameter);
        }
    }
}
