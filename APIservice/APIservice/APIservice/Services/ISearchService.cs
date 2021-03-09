using System.Collections.Generic;
using APIservice.Models;
using APIservice.Repositories;

namespace APIservice.Services
{
    public interface ISearchService
    {
        List<FoodResultView> PerformSearch(string searchRequest, IRepository<IEntity> dbRepository, out string parsedMeal, out string parsedLocation);
    }
}