using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IClientService
    {
        string AddClientAndAddress(string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode);
        bool UpdateClientAndAddress(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode);
        bool RemoveClient(int id);
        ClientsForListDTO GetClientsBySurname(string? surname);
        ClientDetailsDTO GetClientById(int id);        
        ClientsForListDTO ShowAllClients();
        ClientsForListDTO GetClientByStreetDetails(string street, string buildingNumber, string city);
    }
}
