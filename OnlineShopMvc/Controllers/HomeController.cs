using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.Inf;
using OnlineShopMvc.Models;
using OnlineShopMVC.Infrastructure;
using System.Diagnostics;

namespace OnlineShopMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //Seeder seeder = new Seeder();
            //seeder.SeedData();
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