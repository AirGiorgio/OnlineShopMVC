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

   
   

    }
}
