using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;

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
        public IActionResult ViewOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
            _logger.LogInformation("W ViewOrders");
            var Orders = _orderService.GetOrders(pageSize, pageNo, orderDate, min, max, value);
            return View(Orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            _logger.LogInformation("W OrderDetails");
            var Order = _orderService.GetOrderById(id);
            return View(Order);
        }

        [HttpPost]
        public IActionResult RemoveOrder(int id)
        {
            _logger.LogInformation("W RemoveOrder");
            var status = _orderService.RemoveOrder(id);
            if (status == false)
            {
                TempData["Message"] = "Zamówienie już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto zamówienie";
            }
            return RedirectToAction("ViewOrders");
        }
    }
}