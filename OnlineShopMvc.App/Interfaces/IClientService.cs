using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IClientService
    {
        string AddClient(string? name, string? surname, string? email, string? telephone);
        bool AddorUpdateClientAdress(Client? client, string? street, string? buildingNumber, string? flatNumber, string? city, string? zipCode);
        bool UpdateClient(Client? client, string? name, string? surname, string? email, string? telephone);
        bool RemoveClient(int id);
        ClientsForListDTO GetClientsBySurname(string? surname); 
        ClientDetailsDTO GetClientById(int id);        
        ClientsForListDTO ShowAllClients();  
 
    }
}
