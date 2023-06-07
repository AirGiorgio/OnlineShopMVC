using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class ClientShopController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ILogger<ClientShopController> _logger;
        public ClientShopController(IOrderService orderService, ILogger<ClientShopController> logger, IProductService productService)
        {
            _orderService = orderService;
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddOrder(int id, List<Product> orderProducts)
        {
            var addOrder= _orderService.AddOrder(id, orderProducts);
            if (addOrder == false)
            {
                return BadRequest();
            }
            else return Ok("Zamówienie zostało złożone");
        }
        [HttpPost]
        public IActionResult AddToCart(int id, List<Product> orderProducts)
        {
            var addOrder = _orderService.AddOrder(id, orderProducts);
            if (addOrder == false)
            {
                return BadRequest();
            }
            else return Ok("Zamówienie zostało złożone");
        }
        [HttpGet]
        public IActionResult ViewProducts(int? pageSize, int? pageNo, int? categoryId, List<int> searchTags, decimal? min, decimal? max, string? name)
        {
            var products = _productService.GetAllProducts(pageSize, pageNo, categoryId, searchTags, min, max, name);
            return View(products);

        }
    }
}
