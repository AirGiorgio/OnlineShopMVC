using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class AdminClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;

        private readonly ILogger<ClientOrdersController> _logger;
        public AdminClientController(IClientService clientService, ILogger<ClientOrdersController> logger, IAddressService addressService)
        {
            _clientService = clientService;
            _logger = logger;
            _addressService = addressService;
        }
        [HttpGet]
        public IActionResult ViewClients()
        {
            var Clients = _clientService.ShowAllClients();
            if (Clients != null)
            {
                return View(Clients);
            }
            else return NotFound();

        }
        [HttpGet]
        public IActionResult ViewClientsByAddress(string? street, string? buildingNumber, string? city)
        {
            var clients = _addressService.GetClientByStreetDetails(street, buildingNumber, city);
            if (clients != null)
            {
                return View("ViewClients", clients);
            }
            else return View(clients); ;
        }

        [HttpGet]
        public IActionResult ViewClientsBySurname(string? surname)
        {
            var clientsFound = _clientService.GetClientsBySurname(surname);
            if (clientsFound != null)
            {
                return View("ViewClients",clientsFound);
            }
            return View(clientsFound);
        }

        [HttpPost]
        public IActionResult ClientDetails(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
            {
                return BadRequest();
            }
            else return View(client);
        }

        [HttpPost]
        public IActionResult UpdateClient(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode)
        {
            var status = _clientService.UpdateClientAndAddress(id, name, surname, email, telephone, street, buildingNumber, flatNumber, city, zipCode);
       
            if (status ==false)
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewClients");
        }

        [HttpPost]
        public IActionResult RemoveClient(int id)
        {
            var ClientDeleted = _clientService.RemoveClient(id);
            if (ClientDeleted == false)
            {
                return NotFound();
            }
            else return RedirectToAction("ViewClients", "AdminClient");
        }
    
    }
}
