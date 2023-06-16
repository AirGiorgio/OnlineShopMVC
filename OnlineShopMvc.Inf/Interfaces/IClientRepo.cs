using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IClientRepo
    {
        bool RemoveClient(int id);
        string AddClientAndAddress(Client client);
        string UpdateClientAndAddress(Client client);
        IQueryable GetClientsBySurname(string surname);
        Client GetClientById(int id);
        IQueryable ShowAllClients();
        IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city);
    }
}
