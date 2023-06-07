using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class ClientDataController : Controller
    {
        private readonly IClientService _clientService;

        private readonly ILogger<ClientDataController> _logger;
        public ClientDataController(IClientService clientService, ILogger<ClientDataController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult RegisterClient(string? name, string? surname, string? email, string? telephone,
        string? street, string? buildingNumber, string? flatNumber, string? city, string? zipCode)
        {
                var status = _clientService.AddClientAndAddress(name, surname, email, telephone, street, buildingNumber, flatNumber, city, zipCode);
                TempData["Message"] = status;
                return RedirectToAction("ClientDetails","ClientData");
         }
        [HttpGet]
        public IActionResult RegisterClient()
        {
            return View("AddClient", "ClientData");
        }


        [HttpGet]
        public IActionResult ViewClientOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
            //var Orders = _orderService.ViewHistory(pageSize, pageNo, orderDate, min, max, value);
            return View("raa");

        }

        [HttpPost]
        public IActionResult UpdateClient(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode)
        {
            var status = _clientService.UpdateClientAndAddress(id, name, surname, email, telephone, street, buildingNumber, flatNumber, city, zipCode);

            TempData["Message"] = status;
            return RedirectToAction("ClientDatails", "ClientData");
        }

        [HttpPost]
        public IActionResult RemoveClient(int id)
        {
            var status = _clientService.RemoveClient(id);
            if (status == false)
            {
                TempData["Message"] = "Klient już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto konto";
            }
            TempData["Message"] = status;
            return RedirectToAction("ViewProducts", "ClientShop");
        }


    }
}
