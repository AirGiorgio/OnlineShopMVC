using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;

        private readonly ILogger<ClientController> _logger;
        public ClientController(IClientService clientService, ILogger<ClientController> logger, IAddressService addressService)
        {
            _clientService = clientService;
            _logger = logger;
            _addressService = addressService;
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
        public IActionResult AddNewAdressForClient(Client client, string? street, string? buildingNumber, string? flatNumber, string? city, string? zipCode)
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
