using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Uniswap.SmartContracts.Factory.CQS
{


    public partial class FactoryDeployment : FactoryDeploymentBase
    {
        public FactoryDeployment() : base(BYTECODE) { }
        public FactoryDeployment(string byteCode) : base(byteCode) { }
    }

    public class FactoryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x6103f056600035601c52740100000000000000000000000000000000000000006020526f7fffffffffffffffffffffffffffffff6040527fffffffffffffffffffffffffffffffff8000000000000000000000000000000060605274012a05f1fffffffffffffffffffffffffdabf41c006080527ffffffffffffffffffffffffed5fa0e000000000000000000000000000000000060a05263538a3f0e60005114156100ed57602060046101403734156100b457600080fd5b60043560205181106100c557600080fd5b50600054156100d357600080fd5b60006101405114156100e457600080fd5b61014051600055005b631648f38e60005114156102bf576020600461014037341561010e57600080fd5b600435602051811061011f57600080fd5b50600061014051141561013157600080fd5b6000600054141561014157600080fd5b60026101405160e05260c052604060c020541561015d57600080fd5b7f602e600c600039602e6000f33660006000376110006000366000730000000000610180526c010000000000000000000000006000540261019b527f5af41558576110006000f30000000000000000000000000000000000000000006101af5260406101806000f0806101cf57600080fd5b61016052610160513b6101e157600080fd5b610160513014156101f157600080fd5b6000600060246366d3820361022052610140516102405261023c6000610160515af161021c57600080fd5b6101605160026101405160e05260c052604060c020556101405160036101605160e05260c052604060c02055600154600160015401101561025c57600080fd5b6001600154016102a0526102a0516001556101405160046102a05160e05260c052604060c0205561016051610140517f9d42cb017eb05bd8944ab536a8b35bc68085931dd5f4356489801453923953f960006000a36101605160005260206000f3005b6306f2bf62600051141561030e57602060046101403734156102e057600080fd5b60043560205181106102f157600080fd5b5060026101405160e05260c052604060c0205460005260206000f3005b6359770438600051141561035d576020600461014037341561032f57600080fd5b600435602051811061034057600080fd5b5060036101405160e05260c052604060c0205460005260206000f3005b63aa65a6c0600051141561039a576020600461014037341561037e57600080fd5b60046101405160e05260c052604060c0205460005260206000f3005b631c2bbd1860005114156103c05734156103b357600080fd5b60005460005260206000f3005b639f181b5e60005114156103e65734156103d957600080fd5b60015460005260206000f3005b60006000fd5b6100046103f0036100046000396100046103f0036000f3";
        public FactoryDeploymentBase() : base(BYTECODE) { }
        public FactoryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class InitializeFactoryFunction : InitializeFactoryFunctionBase { }

    [Function("initializeFactory")]
    public class InitializeFactoryFunctionBase : FunctionMessage
    {
        [Parameter("address", "template", 1)]
        public virtual string Template { get; set; }
    }

    public partial class CreateExchangeFunction : CreateExchangeFunctionBase { }

    [Function("createExchange", "address")]
    public class CreateExchangeFunctionBase : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class GetExchangeFunction : GetExchangeFunctionBase { }

    [Function("getExchange", "address")]
    public class GetExchangeFunctionBase : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class GetTokenFunction : GetTokenFunctionBase { }

    [Function("getToken", "address")]
    public class GetTokenFunctionBase : FunctionMessage
    {
        [Parameter("address", "exchange", 1)]
        public virtual string Exchange { get; set; }
    }

    public partial class GetTokenWithIdFunction : GetTokenWithIdFunctionBase { }

    [Function("getTokenWithId", "address")]
    public class GetTokenWithIdFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "token_id", 1)]
        public virtual BigInteger Token_id { get; set; }
    }

    public partial class ExchangeTemplateFunction : ExchangeTemplateFunctionBase { }

    [Function("exchangeTemplate", "address")]
    public class ExchangeTemplateFunctionBase : FunctionMessage
    {

    }

    public partial class TokenCountFunction : TokenCountFunctionBase { }

    [Function("tokenCount", "uint256")]
    public class TokenCountFunctionBase : FunctionMessage
    {

    }

    public partial class NewExchangeEventDTO : NewExchangeEventDTOBase { }

    [Event("NewExchange")]
    public class NewExchangeEventDTOBase : IEventDTO
    {
        [Parameter("address", "token", 1, true )]
        public virtual string Token { get; set; }
        [Parameter("address", "exchange", 2, true )]
        public virtual string Exchange { get; set; }
    }





    public partial class GetExchangeOutputDTO : GetExchangeOutputDTOBase { }

    [FunctionOutput]
    public class GetExchangeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "out", 1)]
        public virtual string Out { get; set; }
    }

    public partial class GetTokenOutputDTO : GetTokenOutputDTOBase { }

    [FunctionOutput]
    public class GetTokenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "out", 1)]
        public virtual string Out { get; set; }
    }

    public partial class GetTokenWithIdOutputDTO : GetTokenWithIdOutputDTOBase { }

    [FunctionOutput]
    public class GetTokenWithIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "out", 1)]
        public virtual string Out { get; set; }
    }

    public partial class ExchangeTemplateOutputDTO : ExchangeTemplateOutputDTOBase { }

    [FunctionOutput]
    public class ExchangeTemplateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "out", 1)]
        public virtual string Out { get; set; }
    }

    public partial class TokenCountOutputDTO : TokenCountOutputDTOBase { }

    [FunctionOutput]
    public class TokenCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "out", 1)]
        public virtual BigInteger Out { get; set; }
    }
}
