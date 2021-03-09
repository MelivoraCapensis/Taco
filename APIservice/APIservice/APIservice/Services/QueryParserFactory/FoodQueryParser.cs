using System;
using System.Collections.Generic;
using System.Linq;

namespace APIservice.Services.QueryParserFactory
{
    public class FoodQueryParser : IQuerryParser
    {
        private string query;
        private readonly string[] splitters = new[] {" in "," within "," inside "," at ", " around ", " nearby "};
        private Dictionary<string,List<string>> foodAliases = new Dictionary<string, List<string>>() { {"Vegetarian", new List<string> {"veg","Vegan"}}};
        public string[] assumptionalFoods;
        public string[] assumptionalLocations;
        public string directFood="";
        public string searchLocation = "";

        public FoodQueryParser(string _query, string[] _assumptionalFoods = null, string[] _assumptionalLocations = null)
        {
            query = _query;
            assumptionalFoods = _assumptionalFoods;
            assumptionalLocations = _assumptionalLocations;
        }

        public string[] Parse()
        {
            if (query != null)
            { 
                var splittedString = query.Split(splitters, StringSplitOptions.None);
                string queriedMeal = splittedString[0];
                string queriedLocation = "";
                //TryToFindAssumptions(queriedMeal, queriedLocation);

                if (splittedString.Length > 1) //Does querry has enought of search data
                {
                    queriedLocation = splittedString[1];

                    directFood = queriedMeal;
                    searchLocation = queriedLocation;
                }
                else
                {
                    directFood = queriedMeal;
                    searchLocation = "suburbs";
                }
            }
            return new []{directFood, searchLocation};
        }

        // ToDo: Implementation of food unification. To be look like google search))
        private void TryToFindAssumptions(string queriedMeal, string queriedLocation)
        {
            if (assumptionalFoods != null && assumptionalFoods.Length > 0)
                if (assumptionalFoods.Contains(queriedMeal))
                    directFood = queriedMeal;
                else
                directFood = foodAliases.Where(x => x.Value.Contains(queriedMeal)).Select(x => x.Key).ToString(); // try to find by alias

                searchLocation = assumptionalLocations.Contains(queriedLocation) ? queriedLocation : "";
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