using System;
using System.Linq;

namespace TacoChallenge.Data.QueryParserFactory
{
    public class FoodQueryParser : IQuerryParser
    {
        private string query;
        private readonly string[] splitters = new[] {" in "," within "," inside "," at ", " around ", " nearby "};
        public string[] assumptionalFoods;
        public string[] assumptionalLocations;
        public string directFood="";
        public string searchLocation = "";

        public FoodQueryParser(string _query, string[] _assumptionalFoods, string[] _assumptionalLocations)
        {
            query = _query;
            assumptionalFoods = _assumptionalFoods;
            assumptionalLocations = _assumptionalLocations;
        }

        public string[] Parse()
        {
            if (query != null)
            { 
                var parsedString = query.Split(splitters, StringSplitOptions.None);
                if (parsedString.Length > 1) //Does querry has enought of search data
                {
                    directFood = assumptionalFoods.Contains(parsedString[0]) ? parsedString[0] : "";
                    searchLocation = assumptionalLocations.Contains(parsedString[1]) ? parsedString[1] : "";
                }
                else
                {
                    directFood = parsedString[0];
                    searchLocation = "suburbs";
                }
            }
            return new []{directFood, searchLocation};
        }
    }
    public class FoodQueryParserQueryParserCreator : QueryParserCreator
    {
        public override IQuerryParser FactoryMethod(string _searchField, string[] _assumptionalItems, string[] _assumptionalAreas)
        {
            return new FoodQueryParser(_searchField, _assumptionalItems, _assumptionalAreas);
        }
    }
}