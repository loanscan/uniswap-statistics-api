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
using Uniswap.SmartContracts.DSErc20.CQS;

namespace Uniswap.SmartContracts.DSErc20.Service
{
    public partial class DSErc20Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DSErc20Deployment dSErc20Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DSErc20Deployment>().SendRequestAndWaitForReceiptAsync(dSErc20Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DSErc20Deployment dSErc20Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DSErc20Deployment>().SendRequestAsync(dSErc20Deployment);
        }

        public static async Task<DSErc20Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DSErc20Deployment dSErc20Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, dSErc20Deployment, cancellationTokenSource);
            return new DSErc20Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DSErc20Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, byte[]>(nameFunction, blockParameter);
        }

        
        public Task<byte[]> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, byte[]>(null, blockParameter);
        }

        public Task<string> StopRequestAsync(StopFunction stopFunction)
        {
             return ContractHandler.SendRequestAsync(stopFunction);
        }

        public Task<string> StopRequestAsync()
        {
             return ContractHandler.SendRequestAsync<StopFunction>();
        }

        public Task<TransactionReceipt> StopRequestAndWaitForReceiptAsync(StopFunction stopFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(stopFunction, cancellationToken);
        }

        public Task<TransactionReceipt> StopRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<StopFunction>(null, cancellationToken);
        }

        public Task<string> SetOwnerRequestAsync(SetOwnerFunction setOwnerFunction)
        {
             return ContractHandler.SendRequestAsync(setOwnerFunction);
        }

        public Task<TransactionReceipt> SetOwnerRequestAndWaitForReceiptAsync(SetOwnerFunction setOwnerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setOwnerFunction, cancellationToken);
        }

        public Task<string> SetOwnerRequestAsync(string owner_)
        {
            var setOwnerFunction = new SetOwnerFunction();
                setOwnerFunction.Owner_ = owner_;
            
             return ContractHandler.SendRequestAsync(setOwnerFunction);
        }

        public Task<TransactionReceipt> SetOwnerRequestAndWaitForReceiptAsync(string owner_, CancellationTokenSource cancellationToken = null)
        {
            var setOwnerFunction = new SetOwnerFunction();
                setOwnerFunction.Owner_ = owner_;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setOwnerFunction, cancellationToken);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
        {
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(string src, string dst, BigInteger wad)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.Src = src;
                transferFromFunction.Dst = dst;
                transferFromFunction.Wad = wad;
            
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string src, string dst, BigInteger wad, CancellationTokenSource cancellationToken = null)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.Src = src;
                transferFromFunction.Dst = dst;
                transferFromFunction.Wad = wad;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<BigInteger> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, BigInteger>(decimalsFunction, blockParameter);
        }

        
        public Task<BigInteger> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> SetNameRequestAsync(SetNameFunction setNameFunction)
        {
             return ContractHandler.SendRequestAsync(setNameFunction);
        }

        public Task<TransactionReceipt> SetNameRequestAndWaitForReceiptAsync(SetNameFunction setNameFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setNameFunction, cancellationToken);
        }

        public Task<string> SetNameRequestAsync(byte[] name_)
        {
            var setNameFunction = new SetNameFunction();
                setNameFunction.Name_ = name_;
            
             return ContractHandler.SendRequestAsync(setNameFunction);
        }

        public Task<TransactionReceipt> SetNameRequestAndWaitForReceiptAsync(byte[] name_, CancellationTokenSource cancellationToken = null)
        {
            var setNameFunction = new SetNameFunction();
                setNameFunction.Name_ = name_;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setNameFunction, cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        
        public Task<BigInteger> BalanceOfQueryAsync(string src, BlockParameter blockParameter = null)
        {
            var balanceOfFunction = new BalanceOfFunction();
                balanceOfFunction.Src = src;
            
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        public Task<bool> StoppedQueryAsync(StoppedFunction stoppedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StoppedFunction, bool>(stoppedFunction, blockParameter);
        }

        
        public Task<bool> StoppedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StoppedFunction, bool>(null, blockParameter);
        }

        public Task<string> SetAuthorityRequestAsync(SetAuthorityFunction setAuthorityFunction)
        {
             return ContractHandler.SendRequestAsync(setAuthorityFunction);
        }

        public Task<TransactionReceipt> SetAuthorityRequestAndWaitForReceiptAsync(SetAuthorityFunction setAuthorityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAuthorityFunction, cancellationToken);
        }

        public Task<string> SetAuthorityRequestAsync(string authority_)
        {
            var setAuthorityFunction = new SetAuthorityFunction();
                setAuthorityFunction.Authority_ = authority_;
            
             return ContractHandler.SendRequestAsync(setAuthorityFunction);
        }

        public Task<TransactionReceipt> SetAuthorityRequestAndWaitForReceiptAsync(string authority_, CancellationTokenSource cancellationToken = null)
        {
            var setAuthorityFunction = new SetAuthorityFunction();
                setAuthorityFunction.Authority_ = authority_;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAuthorityFunction, cancellationToken);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<byte[]> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, byte[]>(symbolFunction, blockParameter);
        }

        
        public Task<byte[]> SymbolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, byte[]>(null, blockParameter);
        }

        public Task<string> TransferRequestAsync(TransferFunction transferFunction)
        {
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferRequestAsync(string dst, BigInteger wad)
        {
            var transferFunction = new TransferFunction();
                transferFunction.Dst = dst;
                transferFunction.Wad = wad;
            
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string dst, BigInteger wad, CancellationTokenSource cancellationToken = null)
        {
            var transferFunction = new TransferFunction();
                transferFunction.Dst = dst;
                transferFunction.Wad = wad;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> PushRequestAsync(PushFunction pushFunction)
        {
             return ContractHandler.SendRequestAsync(pushFunction);
        }

        public Task<TransactionReceipt> PushRequestAndWaitForReceiptAsync(PushFunction pushFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushFunction, cancellationToken);
        }

        public Task<string> PushRequestAsync(string dst, BigInteger wad)
        {
            var pushFunction = new PushFunction();
                pushFunction.Dst = dst;
                pushFunction.Wad = wad;
            
             return ContractHandler.SendRequestAsync(pushFunction);
        }

        public Task<TransactionReceipt> PushRequestAndWaitForReceiptAsync(string dst, BigInteger wad, CancellationTokenSource cancellationToken = null)
        {
            var pushFunction = new PushFunction();
                pushFunction.Dst = dst;
                pushFunction.Wad = wad;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushFunction, cancellationToken);
        }

        public Task<string> MoveRequestAsync(MoveFunction moveFunction)
        {
             return ContractHandler.SendRequestAsync(moveFunction);
        }

        public Task<TransactionReceipt> MoveRequestAndWaitForReceiptAsync(MoveFunction moveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(moveFunction, cancellationToken);
        }

        public Task<string> MoveRequestAsync(string src, string dst, BigInteger wad)
        {
            var moveFunction = new MoveFunction();
                moveFunction.Src = src;
                moveFunction.Dst = dst;
                moveFunction.Wad = wad;
            
             return ContractHandler.SendRequestAsync(moveFunction);
        }

        public Task<TransactionReceipt> MoveRequestAndWaitForReceiptAsync(string src, string dst, BigInteger wad, CancellationTokenSource cancellationToken = null)
        {
            var moveFunction = new MoveFunction();
                moveFunction.Src = src;
                moveFunction.Dst = dst;
                moveFunction.Wad = wad;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(moveFunction, cancellationToken);
        }

        public Task<string> StartRequestAsync(StartFunction startFunction)
        {
             return ContractHandler.SendRequestAsync(startFunction);
        }

        public Task<string> StartRequestAsync()
        {
             return ContractHandler.SendRequestAsync<StartFunction>();
        }

        public Task<TransactionReceipt> StartRequestAndWaitForReceiptAsync(StartFunction startFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startFunction, cancellationToken);
        }

        public Task<TransactionReceipt> StartRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<StartFunction>(null, cancellationToken);
        }

        public Task<string> AuthorityQueryAsync(AuthorityFunction authorityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AuthorityFunction, string>(authorityFunction, blockParameter);
        }

        
        public Task<string> AuthorityQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AuthorityFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        
        public Task<BigInteger> AllowanceQueryAsync(string src, string guy, BlockParameter blockParameter = null)
        {
            var allowanceFunction = new AllowanceFunction();
                allowanceFunction.Src = src;
                allowanceFunction.Guy = guy;
            
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        public Task<string> PullRequestAsync(PullFunction pullFunction)
        {
             return ContractHandler.SendRequestAsync(pullFunction);
        }

        public Task<TransactionReceipt> PullRequestAndWaitForReceiptAsync(PullFunction pullFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pullFunction, cancellationToken);
        }

        public Task<string> PullRequestAsync(string src, BigInteger wad)
        {
            var pullFunction = new PullFunction();
                pullFunction.Src = src;
                pullFunction.Wad = wad;
            
             return ContractHandler.SendRequestAsync(pullFunction);
        }

        public Task<TransactionReceipt> PullRequestAndWaitForReceiptAsync(string src, BigInteger wad, CancellationTokenSource cancellationToken = null)
        {
            var pullFunction = new PullFunction();
                pullFunction.Src = src;
                pullFunction.Wad = wad;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pullFunction, cancellationToken);
        }
    }
}
