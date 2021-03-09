using System.Collections.Generic;
using System.Linq;
using APIservice.Models;
using APIservice.Repositories;
using APIservice.Services.QueryParserFactory;

namespace APIservice.Services
{
    public class SearchService : ISearchService
    {
        public List<FoodResultView> PerformSearch(string searchRequest, IRepository<IEntity> dbRepository, out string parsedMeal, out string parsedLocation)
        {
                if (searchRequest != null && dbRepository != null)
                {
                    //TODO: Make it as method and make a unit tests on it
                    QueryParserCreator foodQueryParser = new FoodQueryParserQueryParserCreator();
                    dbRepository = new JsonRepository<IEntity>();
                    var restaurants = dbRepository.GetAllRecords().Cast<Resturant>().ToList();
                    var resturantsWithRequestedFood = new List<Resturant>();
                    var parsedData = foodQueryParser.DoParse(searchRequest, null, null);

                // With custom food/location assumptions set
                //string[] parsedData = foodQueryParser.DoParse(searchField, new string[] { "Taco", "Vegetarian", "Grill" }, new string[] { "Cape Town", "Johannesburg" });

                    parsedMeal = parsedData[0];
                    parsedLocation = parsedData[1];

                    // Get restaurants, witch has searched meal
                    foreach (var resturant in restaurants)
                    foreach (var category in resturant.Categories)
                        if (category.Name.Contains(parsedMeal) &&
                            resturantsWithRequestedFood.Where(x => x.Id == resturant.Id).ToList().Count == 0
                        ) // to avoid added restaurants duplication
                            resturantsWithRequestedFood.Add(resturant); // Select restaurant with requested meal
                        else
                            foreach (var menuItem in category.MenuItems)
                                if (menuItem.Name.Contains(parsedMeal) &&
                                    resturantsWithRequestedFood.Where(x => x.Id == resturant.Id).ToList().Count == 0
                                ) // to avoid added restaurants duplication
                                    resturantsWithRequestedFood.Add(resturant); // Select restaurant with requested meal

                    var resturantsOnlyWithRequestedFood = new List<FoodResultView>();
                    // Get restaurants, witch has only searched meal. Just searched meal
                    foreach (var resturant in restaurants)
                    {
                        var restaurantViewItem = new FoodResultView() { Categories = new List<Category>() };
                        foreach (var category in resturant.Categories)
                        {
                            if (category.Name.Contains(parsedMeal))
                            {
                                //Case when category name contains searched meal->add all menu items
                                restaurantViewItem.Categories.Add(new Category()
                                {
                                    Name = category.Name,
                                    MenuItems = category.MenuItems
                                });
                                restaurantViewItem.Name = resturant.Name;
                                restaurantViewItem.Suburb = resturant.Suburb;
                                restaurantViewItem.Rank = resturant.Rank;
                                restaurantViewItem.LogoPath = resturant.LogoPath;
                                continue;
                            }

                            var categoryViewItem = new Category()
                            { Name = category.Name, MenuItems = new List<MenuItem>() };

                            foreach (var menuItem in category.MenuItems)
                                if (menuItem.Name.Contains(parsedMeal)) // to avoid added restaurants duplication
                                    categoryViewItem.MenuItems.Add(menuItem);

                            if (categoryViewItem.MenuItems.Any())
                            {
                                restaurantViewItem.Categories.Add(categoryViewItem);
                                restaurantViewItem.Name = resturant.Name;
                                restaurantViewItem.Suburb = resturant.Suburb;
                                restaurantViewItem.Rank = resturant.Rank;
                                restaurantViewItem.LogoPath = resturant.LogoPath;
                            }
                        }

                        if (restaurantViewItem.Categories.Any())
                            resturantsOnlyWithRequestedFood.Add(restaurantViewItem);
                    }

                    resturantsWithRequestedFood.Sort(new FoodNameComporator(parsedMeal)); //sort by count of relevant meal 
                    resturantsOnlyWithRequestedFood.Sort(new FoodVeiwNameComporator(parsedMeal));

                    //
                    //OR same but LINQ: Get restaurants, witch has searched meal, sorted by count of relevant meal than by rank
                    //
                    /*var restaurantsRelevantSorted = restaurants.Select(x =>
                                    new
                                    {
                                        _countOfAvailableMeal = x.Categories.SelectMany(c => c.MenuItems)
                                            .Count(cI => cI.Name.Contains(parsedMeal)),
                                        _restaurantWithSearchedMeal = x
                                    })
                                .Where(x => x._countOfAvailableMeal > 0)
                                .OrderByDescending(x => x._countOfAvailableMeal).ThenBy(x =>x._restaurantWithSearchedMeal.Rank )
                                .Select(r => r._restaurantWithSearchedMeal)
                                .ToList();*/
                    //ToDo_Ends

                    #region Dummy data for customize view

                    //TODO: Make an adapter for Translation Resturant to FoodResultView or just translate it in FoodResultView ctor
                    /*List<FoodResultView> dummyFoundFoodItems = new List<FoodResultView>();
                            var rest1 = (Resturant)dbRepository.GetItem(2380);
                            var rest2 = (Resturant)dbRepository.GetItem(2438);
                            var rest3 = (Resturant)dbRepository.GetItem(935);
                            FoodResultView result1 = new FoodResultView(){Name = rest1.Name, Suburb = rest1.Suburb, Rank = rest1.Rank, FoodMenuItems = rest1.Categories[0].MenuItems, LogoPath = rest1.LogoPath};
                            FoodResultView result2 = new FoodResultView() { Name = rest2.Name, Suburb = rest2.Suburb, Rank = rest2.Rank, FoodMenuItems = rest2.Categories[0].MenuItems, LogoPath = rest2.LogoPath };
                            FoodResultView result3 = new FoodResultView() { Name = rest3.Name, Suburb = rest3.Suburb, Rank = rest3.Rank, FoodMenuItems = rest3.Categories[0].MenuItems, LogoPath = rest3.LogoPath };

                            dummyFoundFoodItems.Add(result1);
                            dummyFoundFoodItems.Add(result2);
                            dummyFoundFoodItems.Add(result3);

                            ViewBag.FoundFood = dummyFoundFoodItems;*/

                    #endregion

                    return resturantsOnlyWithRequestedFood;
                }

                parsedMeal = null;
                parsedLocation = null;
                return null;
            }
    }
}