namespace Uniswap.Data.Entities
{
    public interface ILastBlockFetchedByExchangeEntity : IEntityBase<string>
    {
        ulong LastBlock { get; set; }
    }
}