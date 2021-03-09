using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TacoChallenge.Models;

namespace TacoChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public string Message { get; set; }

        public HomeController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index(string searchField)
        {
            Message = $"Index page called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            _logger.LogDebug(Message);

            if (searchField != null)
            {
                string apiUrl = "http://localhost:55667/main/FoodRestuarantsByQuery";
                string urlParams = "?searchRequest=" + searchField;
                List<FoodResultView> resturantsOnlyWithRequestedFood;
                string parsedMeal = "";
                string parsedLocation = "";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParams).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var jsonDataContainer = response.Content.ReadAsAsync<FoodResultsContainer>().Result;  
                    resturantsOnlyWithRequestedFood = jsonDataContainer.MealViewList;
                    parsedMeal = jsonDataContainer.ParsedMeal;
                    parsedLocation = jsonDataContainer.ParsedLocation;
                }
                else
                {
                    resturantsOnlyWithRequestedFood= new List<FoodResultView>();
                    Message = apiUrl + urlParams + " - Failed to retrieve data";
                    _logger.LogError(Message);
                }

                ViewBag.SearchedFoodRequest = parsedMeal;
                ViewBag.SearchedLocationRequest = parsedLocation;
                
                ViewBag.FoundRestaurantsWithFood = resturantsOnlyWithRequestedFood;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(double orderTotalSum)
        {
            Message = $"Index page called by POST at {DateTime.UtcNow.ToLongTimeString()}";
            ViewBag.OrderSum = orderTotalSum;

            _logger.LogInformation(Message);
            return View("Success");
        }

        public ActionResult SearchFood(string searchRequest)
        {
            List<FoodResultView> foodResultView = new List<FoodResultView>();
            return PartialView("_FoodResult");
        }

        public IActionResult Success()
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
