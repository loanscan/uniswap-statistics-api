using Moq.AutoMock;

namespace Uniswap.Statistics.Api.Core.Tests
{
    public abstract class TestsClassBase
    {
        public AutoMocker Mocker { get; set; } = new AutoMocker();
    }
}