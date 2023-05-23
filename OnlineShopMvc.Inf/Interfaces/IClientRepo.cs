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
        string AddClientAndAddress(Address adres, Client client);
        bool UpdateClientAndAddress(Address adress, Client client, int id);
        IQueryable GetClientsBySurname(string surname);
        Client GetClientById(int id);
        IQueryable ShowAllClients();

    }
}
