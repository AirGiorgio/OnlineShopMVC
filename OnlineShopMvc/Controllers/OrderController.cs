using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddOrder(Client? client, List<Product> orderProducts)
        {
            var addOrder= _orderService.AddOrder(client, orderProducts);
            if (addOrder == false)
            {
                return BadRequest();
            }
            else return Ok("Zamówienie zostało złożone");
        }

   
        [HttpGet]
        public IActionResult GetOrdersFromDate(DateTime? orderDate, Client? client)
        {
            var Orders = _orderService.GetOrdersFromDate(orderDate, client);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        
        [HttpGet]
        public IActionResult GetOrdersByOrderDate(Client? client)
        {
            var Orders = _orderService.GetOrdersByOrderDate(client);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
     
        [HttpGet]
        public IActionResult GetOrdersFromValue(Client? client, int? min, int? max)
        {
            var Orders = _orderService.GetOrdersFromValue(client, min, max);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
     
        [HttpGet]
        public IActionResult GetOrdersByValue(Client? client)
        {
            var Orders = _orderService.GetOrdersByValue(client);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }

    }
}
