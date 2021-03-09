using APIservice.Services.QueryParserFactory;
using Xunit;

namespace Tests
{
    public class QueryParserTests
    {
        [Fact]
        public void QueryParseEquilityTest()
        {
            QueryParserCreator foodQueryParser = new FoodQueryParserQueryParserCreator();
            string[] parsedData = foodQueryParser.DoParse("Taco in Johannesburg", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });

            Assert.Equal("Taco", parsedData[0]);
            Assert.Equal("Johannesburg", parsedData[1]);

            parsedData = foodQueryParser.DoParse("qwer", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });
            Assert.Equal("qwer", parsedData[0]);
            Assert.Equal("suburbs", parsedData[1]);

            parsedData = foodQueryParser.DoParse("Taco or Johannesburg", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });
            Assert.Equal("Taco or Johannesburg", parsedData[0]);
            Assert.Equal("suburbs", parsedData[1]);
        }

        [Fact]
        public void QueryParseNotNulEmptyTest()
        {
            QueryParserCreator foodQueryParser = new FoodQueryParserQueryParserCreator();
            string[] parsedData = foodQueryParser.DoParse("Taco in Johannesburg", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });

            Assert.NotNull(parsedData[0]);
            Assert.NotNull(parsedData[1]);
            Assert.NotNull(parsedData[0]);
            Assert.NotNull(parsedData[1]);

            parsedData = foodQueryParser.DoParse("qwe", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });

            Assert.NotNull(parsedData[0]);
            Assert.NotNull(parsedData[1]);
            Assert.NotEmpty(parsedData[0]);
            Assert.NotEmpty(parsedData[1]);

            parsedData = foodQueryParser.DoParse("Taco or Johannesburg", new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });
            Assert.NotNull(parsedData[0]);
            Assert.NotNull(parsedData[1]);
            Assert.NotEmpty(parsedData[0]);
            Assert.NotEmpty(parsedData[1]);
        }
    }

}
