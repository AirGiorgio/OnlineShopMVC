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
        bool UpdateClient(Client client);
        bool RemoveClient(int id);
        string AddClient(Client client);
        bool AddAdress(Address adress, Client client);
        IQueryable GetClientsBySurname(string surname);
        Client GetClientById(int id);
        IQueryable ShowAllClients();


    }
}
