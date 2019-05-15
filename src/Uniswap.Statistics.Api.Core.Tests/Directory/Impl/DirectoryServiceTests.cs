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

            await service.GetAllDirectoriesAsync();

            Mocker.GetMock<IExchangeRepository>().Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}