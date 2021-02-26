using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TacoChallenge.Data;
using TacoChallenge.Data.QueryParcerFactory;
using TacoChallenge.Models;

namespace TacoChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public string Message { get; set; }
        private IRepository<IEntity> dbRepository;

        public HomeController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<HomeController>();

            dbRepository = new JsonRepository<IEntity>();
        }

        public IActionResult Index(string searchField)
        {
            Message = $"Index page called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            _logger.LogDebug(Message);

            if (searchField != null)
            {
                //TODO: Make it as method and make a unit tests on it
                QueryParcerCreator foodQueryParcer = new FoodQueryParcerQueryParcerCreator();
                string[] parcedData = foodQueryParcer.DoParce(searchField, new string[] { "Taco", "Grill" }, new string[] { "Cape Town", "Johannesburg" });
                ViewBag.SearchedFoodRequest = parcedData[0];
                ViewBag.SearchedLocationRequest = parcedData[1];
                //ToDo_Ends

                #region Dummy data for customize view
                //TODO: Make an adapter for Translation Resturant to FoodResultView or just translate it in FoodResultView ctor
                List<FoodResultView> dummyFoundFoodItems = new List<FoodResultView>();
                var rest1 = (Resturant)dbRepository.GetItem(2380);
                var rest2 = (Resturant)dbRepository.GetItem(2438);
                var rest3 = (Resturant)dbRepository.GetItem(935);
                FoodResultView result1 = new FoodResultView(){ResturantName = rest1.Name, Suburb = rest1.Suburb, Rank = rest1.Rank, FoodMenuItems = rest1.Categories[0].MenuItems, LogoPath = rest1.LogoPath};
                FoodResultView result2 = new FoodResultView() { ResturantName = rest2.Name, Suburb = rest2.Suburb, Rank = rest2.Rank, FoodMenuItems = rest2.Categories[0].MenuItems, LogoPath = rest2.LogoPath };
                FoodResultView result3 = new FoodResultView() { ResturantName = rest3.Name, Suburb = rest3.Suburb, Rank = rest3.Rank, FoodMenuItems = rest3.Categories[0].MenuItems, LogoPath = rest3.LogoPath };

                dummyFoundFoodItems.Add(result1);
                dummyFoundFoodItems.Add(result2);
                dummyFoundFoodItems.Add(result3);

                ViewBag.FoundFood = dummyFoundFoodItems;
                #endregion

                
            }
            return View();
        }
        /*[HttpPost]
        public IActionResult Index(string searchField)
        {
            Message = $"Index page called by GET at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            if (searchField != null)
            {
                var searchRequestArraySplit = searchField.Split(" in ");
                ViewBag.SearchedFoodRequest = searchRequestArraySplit[0];
                ViewBag.SearchedLocationRequest = searchRequestArraySplit[1];
            }

            return View();
        }*/

        public ActionResult SearchFood(string searchRequest)
        {
            List<FoodResultView> foodResultView = new List<FoodResultView>();
            return PartialView("_FoodResult");
        }

        public IActionResult About()
        {
            Message = $"About page called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            ViewData["Message"] = "Taco Challenge description page.";
            var item = (Resturant)dbRepository.GetItem(2380);

            return View(item);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Taco Challenge contact page.";

            List<IEntity> records;
            try
            {
                records = dbRepository.GetAllRecords();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
            return View(records);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
