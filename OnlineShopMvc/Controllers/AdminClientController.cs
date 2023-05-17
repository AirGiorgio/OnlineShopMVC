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

        private readonly ILogger<ClientController> _logger;
        public AdminClientController(IClientService clientService, ILogger<ClientController> logger, IAddressService addressService)
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
        public IActionResult GetClientByStreetDetails(string? street, string? buildingNumber, string? city)
        {
            var clients = _addressService.GetClientByStreetDetails(street, buildingNumber, city);
            if (clients != null)
            {
                return View(clients);
            }
            else return NotFound();
        }
       

        [HttpGet]
        public IActionResult GetClientsBySurname(string? surname)
        {
            var clientsFound = _clientService.GetClientsBySurname(surname);
            return View(clientsFound);
        }

        [HttpPost] //or put
        public IActionResult AddClient(string? name, string? surname, string? email, string? telephone)
        {
            var Client = _clientService.AddClient(name, surname, email, telephone);
            if (Client.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return Ok("Rejestracja udana");
        }

        [HttpPut]  
        public IActionResult AddAddress(Client client, string? street, string? buildingNumber, string? flatNumber, string? city, string? zipCode)
        {
            var adress = _clientService.AddorUpdateClientAdress(client, street, buildingNumber, flatNumber, city, zipCode);
            if (adress == false)
            {
                return BadRequest();
            }
            else return Ok("Udało się zaktualizować adres");
        }

        [HttpPatch]
        public IActionResult UpdateClient(Client? client, string? name, string? surname, string? email, string? telephone)
        {
            var Client = _clientService.UpdateClient(client, name, surname, email, telephone);
            if (Client == false)
            {
                return BadRequest();
            }
            else return Ok("Udało się zaktualizować dane klienta");
        }

        [HttpDelete]
        public IActionResult DeleteClient(Client client)
        {
            var ClientDeleted = _clientService.RemoveClient(client.Id);
            if (ClientDeleted == false)
            {
                return NotFound();
            }
            else return Ok("Usunięto konto klienta");
        }
    
    }
}
