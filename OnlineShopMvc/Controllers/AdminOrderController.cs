using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly ILogger<AdminOrderController> _logger;
        public AdminOrderController(IOrderService orderService, ILogger<AdminOrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllOrdersFromDate(DateTime? orderDate)
        {
            var Orders = _orderService.GetAllOrdersFromDate(orderDate);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult ViewOrders()
        {
            var Orders = _orderService.GetOrdersByOrderDate();
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetOrderById(int id)
        {
            var Order = _orderService.GetOrderById(id);
            if (Order != null)
            {
                return View(Order);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetOrdersFromValue(int? min, int? max)
        {
            var Orders = _orderService.GetOrdersFromValue(min, max);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetOrdersByValue()
        {
            var Orders = _orderService.GetOrdersByValue();
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }
        [HttpPatch]
        public IActionResult RemoveOrder(Order order)
        {
            var orderDeleted = _orderService.RemoveOrder(order.Id);
            if (orderDeleted == false)
            {
                return NotFound();
            }
            else return Ok("Zamówienie zostało usunięte");

        }
    }
}
