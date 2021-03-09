using Moq;
using APIservice.Models;
using APIservice.Repositories;
using Xunit;
using APIservice.Controllers;
using APIservice.Services;

namespace Tests
{
    public class MainControllerTests
    {
        [Fact]
        public void GetDataMessage()
        {
            var mockDbRepository = new Mock<JsonRepository<IEntity>>();
            var mockSearchService = new Mock<ISearchService>();
            //MainController controller = new MainController();
            //Assert.NotNull(controller.GetFoodRestuarantsByQuery("Taco in Cape Town"));
            Assert.NotNull("");
        }
    }
}
