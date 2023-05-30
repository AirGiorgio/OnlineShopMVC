using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMVC.Domain.Model;
using static NuGet.Packaging.PackagingConstants;

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
            var Orders = _orderService.GetOrders(pageSize,pageNo,orderDate,min,max,value);
            if (Orders != null)
            {
                return View(Orders);
            }
            else return NotFound();
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)  
        {
            var Order = _orderService.GetOrderById(id);
            if (Order != null)
            {
                return View(Order);
            }
            else return BadRequest();
        }

        [HttpPost]
        public IActionResult RemoveOrder(int id)  
        {
            var orderDeleted = _orderService.RemoveOrder(id);
            if (orderDeleted == false)
            {
                return NotFound();
            }
            else return RedirectToAction("ViewOrders");
        }
    }
}
