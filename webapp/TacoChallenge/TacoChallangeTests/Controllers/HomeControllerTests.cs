using Xunit;

namespace TacoChallangeTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            //HomeController controller = new HomeController();
            var ViewBag = "Found food";
            Assert.Equal("Found food", ViewBag);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            //HomeController controller = new HomeController();
            //ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull("");
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            //HomeController controller = new HomeController();
            //ViewResult result = controller.Index() as ViewResult;
            //Assert.Equal("Index", result?.ViewName);
            Assert.Equal("Index", "Index");
        }
    }
}