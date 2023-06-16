using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMVC.Domain.Model;
using System.Drawing.Printing;

namespace OnlineShopMvc.Controllers
{
    public class AdminClientController : Controller
    {
        private readonly IClientService _clientService;

        private readonly ILogger<ClientDataController> _logger;
        public AdminClientController(IClientService clientService, ILogger<ClientDataController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ViewClients(int? pageSize, int? pageNo, string? street, string? buildingNumber, string? city, string? surname)
        {
            _logger.LogInformation("W ViewClients");
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
            _logger.LogInformation("W ClientDetails");
            var client = _clientService.GetClientById(id);
            
            return View(client);
        }

        [HttpPost]
        public IActionResult UpdateClient(ClientDetailsDTO client)
        {
            if (!ModelState.IsValid)
            {
                return View("ClientDetails", client);
            }
            _logger.LogInformation("W UpdateClients");
            var status = _clientService.UpdateClientAndAddress(client);

            TempData["Message"] = status;
            return RedirectToAction("ViewClients", "AdminClient");
        }

        [HttpPost]
        public IActionResult RemoveClient(int id)
        {
            _logger.LogInformation("W RemoveClient");
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
