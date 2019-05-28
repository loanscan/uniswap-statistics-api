using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Uniswap.Data.Repositories;
using Uniswap.Statistics.Api.Core.Directory.Impl;

namespace Uniswap.Statistics.Api.Core.Tests.Directory.Impl
{
    [TestOf(typeof(DirectoryService))]

    public class DirectoryServiceTests : TestsClassBase
    {
        [Test]
        public async Task GetAllDirectoriesAsync_Calls_GetAllAsyncIsExecuted()
        {
            var service = Mocker.CreateInstance<DirectoryService>();

            await service.GetDirectoriesAsync(0);

            Mocker.GetMock<IExchangeRepository>().Verify(x => x.GetAsync(0), Times.Once);
        }
    }
}