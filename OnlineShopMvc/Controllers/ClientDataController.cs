using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class ClientDataController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly ILogger<ClientDataController> _logger;
        public ClientDataController(IClientService clientService, ILogger<ClientDataController> logger, IOrderService orderService)
        {
            _clientService = clientService;
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult RegisterClient(ClientDetailsDTO client)
        {
            _logger.LogInformation("W RegisterClient typu Post klienta");
            var status = _clientService.AddClientAndAddress(client);
                TempData["Message"] = status;
                return RedirectToAction("ClientDetails","ClientData");
         }
        [HttpGet]
        public IActionResult RegisterClient()
        {
            _logger.LogInformation("W RegisterClient typu Get klienta");
            return View();
        }
        [HttpGet]
        public IActionResult LoginClient()
        {
            _logger.LogInformation("W LoginClient typu Get klienta");
            return View();
        }
        [HttpGet]
        public IActionResult ClientDetails()
        {
            _logger.LogInformation("W ClientDetails typu Get klienta");
            return View();
        }
        [HttpGet]
        public IActionResult UpdateClient()
        {
            _logger.LogInformation("W UpdateClient typu Get klienta");
            return View();
        }
        [HttpGet]
        public IActionResult ClientOrders()
        {
            _logger.LogInformation("W ClientOrders typu Get klienta");
            return View();
        }

        [HttpPost]
        public IActionResult ClientOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
            _logger.LogInformation("W ClientOrders typu Post klienta");
            var orders = _orderService.GetAllClientOrders(0, pageSize, pageNo, orderDate, min, max, value);
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateClient(ClientDetailsDTO client)
        {
            _logger.LogInformation("W UpdateClient typu Post klienta");
            var status = _clientService.UpdateClientAndAddress(client);

            TempData["Message"] = status;
            return RedirectToAction("ClientDatails", "ClientData");
        }

        [HttpPost]
        public IActionResult RemoveClient(int id)
        {
            _logger.LogInformation("W RemoveClient klienta");
            var status = _clientService.RemoveClient(id);
            if (status == false)
            {
                TempData["Message"] = "To konto już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto konto";
            }
            TempData["Message"] = status;
            return RedirectToAction("Index", "Home");
        }


    }
}
