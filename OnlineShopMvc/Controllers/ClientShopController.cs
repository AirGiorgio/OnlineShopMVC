using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class ClientShopController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly ILogger<ClientShopController> _logger;
        public ClientShopController(IOrderService orderService, ILogger<ClientShopController> logger)
        {
            _orderService = orderService;
            _logger = logger;
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

   
        [HttpGet]
        public IActionResult GetOrdersFromDate(DateTime? orderDate, int id)
        {
            var Orders = _orderService.GetOrdersFromDate(orderDate, id);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        
        [HttpGet]
        public IActionResult GetOrdersByOrderDate(int id)
        {
            var Orders = _orderService.GetOrdersByOrderDate(id);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
     
        [HttpGet]
        public IActionResult GetOrdersFromValue(int id, int? min, int? max)
        {
            var Orders = _orderService.GetOrdersFromValue(id, min, max);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
     
        [HttpGet]
        public IActionResult GetOrdersByValue(int id)
        {
            var Orders = _orderService.GetOrdersByValue(id);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }

    }
}
