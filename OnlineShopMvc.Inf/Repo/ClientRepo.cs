using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamLibraryMVC.Infrastructure.Repositories
{
    public class ClientRepo
    {
        private readonly Context context;

        public ClientRepo(Context context)
        {
            this.context = context;
        }

        public void RemoveClient(int id)
        {
            var client = context.Products.Find(id);
            if (client != null)
            {
                context.Remove(client);
                context.SaveChanges();
            }
        }
        public int AddClient(Client client)
        {
            context.Add(client);
            context.SaveChanges();
            return client.Id;
        }
        public Client GetClientById(int id)
        {
            return context.Clients.SingleOrDefault(i => i.Id == id);
        }
    }
}
