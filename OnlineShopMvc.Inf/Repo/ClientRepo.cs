using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamLibraryMVC.Infrastructure.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly Context context;

        public ClientRepo(Context context)
        {
            this.context = context;
        }
 
        public bool UpdateClient(Client client) 
        {
            var clientFound = GetClientById(client.Id);
            if (clientFound != null)
            {
                clientFound.Id = client.Id;
                clientFound.Name = client.Name;
                clientFound.Surname = client.Surname;
                clientFound.Address = client.Address;
                clientFound.Telephone = client.Telephone;
                clientFound.EmailAdress = client.EmailAdress;
                context.Update(client);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool RemoveClient(int id) 
        {
            var client = GetClientById(id);
            if (client != null)
            {
                context.Remove(client);
                context.Remove(context.Adresses.Where(x => x.ClientId == client.Id));
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public string AddClient(Client client)
        {
            context.Add(client);
            context.SaveChanges();
            return client.ToString(); 
        }

        public bool AddAdress(Address adress, Client client)
        {
            client.Address = adress;
            adress.Client = client;
            context.Add(adress);
            context.SaveChanges();
            return true;
        }
        public IQueryable GetClientsBySurname(string surname)
        {
            return context.Clients.Where(i => i.Name.StartsWith(surname));
        }

        public Client GetClientById(int id) 
        {
            return context.Clients.SingleOrDefault(i => i.Id == id);
        }

        public IQueryable ShowAllClients()
        {
            return context.Clients;
        }
    }
}
