using Azure.Identity;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Repo
{
    public class AddressRepo : IAddressRepo
    {
        private readonly Context context;
        public AddressRepo(Context context)
        {
            this.context = context;
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
