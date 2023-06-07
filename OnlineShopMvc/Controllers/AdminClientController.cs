using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMVC.Domain.Model;
using System.Drawing.Printing;

namespace OnlineShopMvc.Controllers
{
    public class AdminClientController : Controller
    {
        private readonly IClientService _clientService;

        private readonly ILogger<ClientOrdersController> _logger;
        public AdminClientController(IClientService clientService, ILogger<ClientOrdersController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ViewClients(int? pageSize, int? pageNo, string? street, string? buildingNumber, string? city, string? surname)
        {
            var Clients = _clientService.ShowAllClients(pageSize,pageNo,street,buildingNumber,city,surname);
            if (Clients != null)
            {
                return View(Clients);
            }
            else return NotFound();

        }

        [HttpPost]
        public IActionResult ClientDetails(int id)
        {
            var client = _clientService.GetClientById(id);
            
            return View(client);
        }

        [HttpPost]
        public IActionResult UpdateClient(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode)
        {
            var status = _clientService.UpdateClientAndAddress(id, name, surname, email, telephone, street, buildingNumber, flatNumber, city, zipCode);

            TempData["Message"] = status;
            return RedirectToAction("ViewClients", "AdminClient");
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
                TempData["Message"] = "Usunięto klienta i jego adres";
            }
            TempData["Message"] = status;
            return RedirectToAction("ViewClients", "AdminClient");
        }
    
    }
}
