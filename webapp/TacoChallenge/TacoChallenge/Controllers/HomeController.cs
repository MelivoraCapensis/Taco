using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Taco Challenge contact page.";

            return View();
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
