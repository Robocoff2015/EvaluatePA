using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EvaluatePa.Models;

namespace EvaluatePa.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString = @"Server=Localhost\MSSQLLocalDB;Database=EvaluateWork;Trusted_Connection=True";
        private readonly ILogger<HomeController> _logger;
        public static Microsoft.Extensions.Configuration.IConfiguration configuration;
        public HomeController(Microsoft.Extensions.Configuration.IConfiguration config, ILogger<HomeController> logger)
        {
            HomeController.configuration = config;
            _logger = logger;
        }

        public IActionResult Index()
        {
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
