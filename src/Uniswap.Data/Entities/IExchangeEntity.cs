namespace Uniswap.Data.Entities
{
    public interface IExchangeEntity : IEntityBase<string>
    {
        string TokenAddress { get; set; }
        string TokenName { get; set; }
        string TokenSymbol { get; set; }
        int TokenDecimals { get; set; }
        ulong BlockNumber { get; set; }
        decimal EthLiquidity { get; set; }
        decimal TokenLiquidity { get; set; }
        decimal TotalSupply { get; set; }
        string Theme { get; set; }
    }
}