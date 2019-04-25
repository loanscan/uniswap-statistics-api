using System;

namespace Uniswap.Data.Entities
{
    public interface IEntityBase<T>
        where T : IEquatable<T>
    {
        T Id { get; set; }
    }
}