using Microsoft.EntityFrameworkCore;
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
 
        public bool RemoveClient(int id) 
        {
            var client = GetClientById(id);
            if (client != null)
            {
                context.Remove(client);
                context.Remove(context.Addresses.SingleOrDefault(x => x.ClientId == client.Id));
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public IQueryable GetClientsBySurname(string surname)
        {
            return context.Clients.Where(i => i.Surname.StartsWith(surname));
        }

        public Client GetClientById(int id) 
        {
            return context.Clients.Include(x => x.Address).SingleOrDefault(x =>x.Id==id); 
        }

        public IQueryable ShowAllClients()
        {
            return context.Clients;
        }

        public string AddClientAndAddress(Address adres, Client client)
        {
            context.Add(client);
            context.Add(adres);
            context.SaveChanges();
            return client.Name+" "+client.Surname;
        }

        public bool UpdateClientAndAddress(Address adress, Client client, int id)
        {
            var clientF = GetClientById(id);
            if (clientF==null)
            {
                return false;
            }
            clientF.Name = client.Name;
            clientF.Surname = client.Surname;
            clientF.Telephone = client.Telephone;
            clientF.EmailAdress = client.EmailAdress;
            clientF.Address = adress;
            context.SaveChanges();
            return true;
        }
        public Address GetAddressByClientId(int id)
        {
            return context.Addresses.SingleOrDefault(i => i.ClientId == id);
        }

        public IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city)
        {
            IQueryable<Client> query = context.Clients;

            if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(buildingNumber) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.BuildingNumber.StartsWith(buildingNumber) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(buildingNumber))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.BuildingNumber.StartsWith(buildingNumber));
            }
            else if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(buildingNumber) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.BuildingNumber.StartsWith(buildingNumber) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(street))
            {
                query = query.Where(c => c.Address.Street.StartsWith(street));
            }
            else if (!string.IsNullOrEmpty(buildingNumber))
            {
                query = query.Where(c => c.Address.BuildingNumber.StartsWith(buildingNumber));
            }
            else if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.Address.City.StartsWith(city));
            }
            else
            {
                query = context.Clients;
            }
            return query;

        }
    }
}
