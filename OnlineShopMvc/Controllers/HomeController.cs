using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.Filters;
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
        }
        public IActionResult ShopManaging()
        {
            return View();
        }
     
        public IActionResult Index()
        {
            _logger.LogInformation("W Index/Home");
            return View();
        }
        public IActionResult _LoginPartial()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            _logger.LogInformation("W Privacy/Home");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("W Error/Home");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}