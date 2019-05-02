using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Uniswap.Data.Entities;
using Uniswap.Data.Repositories;
using Uniswap.Statistics.Api.Core.Ticker.Impl;

namespace Uniswap.Statistics.Api.Core.Tests.Ticker.Impl
{
    [TestOf(typeof(TickerService))]
    public class TickerServiceTests : TestsClassBase
    {
        [Test]
        public void GetByAddress_WhenNoPurchaseEvents_ExceptionShouldNotBeThrown()
        {
            Mocker
                .Setup<IExchangeEventEntity, ExchangeEventType>(x => x.Type)
                .Returns(ExchangeEventType.AddLiquidity);
            Mocker
                .Setup<IExchangeRepository, Task<IExchangeEntity>>(x =>
                    x.FindByAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Mocker.Get<IExchangeEntity>());
            Mocker
                .Setup<IExchangeEventsRepository, Task<IEnumerable<IExchangeEventEntity>>>(x =>
                    x.GetForLastDayByExchangeAddressAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<IExchangeEventEntity>
                {
                    Mocker.Get<IExchangeEventEntity>()
                });
            var service = Mocker.CreateInstance<TickerService>();

            Assert.DoesNotThrowAsync(async () => await service.GetByAddress(It.IsAny<string>()));
        }
    }
}