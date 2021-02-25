using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TacoChallenge.Data;
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
        public IActionResult Index()
        {
            Message = $"Index page called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            _logger.LogDebug(Message);
            return View();
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
