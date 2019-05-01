using System.Threading.Tasks;

namespace Uniswap.Data.Indexes
{
    public interface IIndexInitializer
    {
        Task Initialize();
    }
}