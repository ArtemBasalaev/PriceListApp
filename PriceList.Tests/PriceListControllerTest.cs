using Microsoft.EntityFrameworkCore;
using Moq;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;
using PriceList.Controllers;
using PriceList.DataAccess;

namespace PriceList.Tests
{
    public class PriceListControllerTest
    {
        [Fact]
        public void TestGetPriceLists()
        {
            var mock = new Mock<GetPriceListsHandler>(new PriceListDbContext(new DbContextOptions<PriceListDbContext>()));

            mock.Setup(handler => handler.HandleAsync())
                .Returns(GetPriceListsAsync());

            var priceListController = new PriceListController();
            var result = priceListController.GetPriceLists(mock.Object)?.Result;

            Assert.IsType<List<PriceListDto>>(result);
            Assert.Equal(GetPriceListsAsync()?.Result.Count, 1);
        }

        private Task<List<PriceListDto>> GetPriceListsAsync()
        {
            return Task.FromResult(new List<PriceListDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Прайс-лист",
                    CreationDate = DateTime.Now
                }
            });
        }
    }
}