using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIservice.Services.QueryParserFactory;
using APIservice.Models;
using APIservice.Repositories;
using APIservice.Services;
using Microsoft.Extensions.Logging;

namespace APIservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILogger _logger;
        public string Message { get; set; }
        private IRepository<IEntity> dbRepository;        

        // GET main
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET main/5
        /*[HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }*/

        // GET main/FoodRestuarantsByQuery?searchRequest=Taco in Cape Town
        [Route("FoodRestuarantsByQuery")]
        [HttpGet]
        public JsonResult GetFoodRestuarantsByQuery(string searchRequest)
        {
            this.dbRepository = new JsonRepository<IEntity>();
            SearchService searcher = new SearchService();
            var parsedMeal ="";
            var parsedLocation = "";
            List<FoodResultView> searchBatchResult = searcher.PerformSearch(searchRequest, this.dbRepository, out parsedMeal, out parsedLocation);
            var res = new FoodResultsContainer();
            res.MealViewList = searchBatchResult;
            res.ParsedMeal = parsedMeal;
            res.ParsedLocation = parsedLocation;

            return new JsonResult(res);
        }

        // POST main/SearchFoodRestuarantsByQuery?searchReq=Taco in Cape Town
        [Route("SearchFoodRestuarantsByQuery")]
        [HttpPost]
        public JsonResult PostSearchFoodRestuarantsByQuery(string searchReq)
        {
            return new JsonResult("Post "+searchReq);
        }
    }
}
