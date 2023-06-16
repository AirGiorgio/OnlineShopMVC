using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
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
        public IActionResult AddOrder(int id, List<ProductDTO> orderProducts)
        {
            _logger.LogInformation("W AddOrder klienta");
            var addOrder = _orderService.AddOrder(id, null);
            return Ok("Zamówienie złożone");
        }
        [HttpPost]
        public IActionResult AddToCart(int id, List<ProductDTO> orderProducts)
        {
            _logger.LogInformation("W AddToCart klienta");
            var addOrder = _orderService.AddOrder(id, null); //AddToCart 
            return RedirectToAction("Zamówienie zostało złożone");
        }
        [HttpGet]
        public IActionResult ViewProducts(int? pageSize, int? pageNo, CategoryDTO searchCategory, List<TagDTO> searchTags, decimal? min, decimal? max, string? name)
        {
            _logger.LogInformation("W ViewProducts klienta");
            var products = _productService.GetAllProducts(pageSize, pageNo, searchCategory, searchTags, min, max, name);
            return View(products);

        }
    }
}
