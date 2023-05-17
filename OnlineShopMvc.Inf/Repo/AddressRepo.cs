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
            return context.Adresses.SingleOrDefault(i => i.ClientId == id);
        }

        public IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city)
        {
            return context.Clients.Where(c =>
            c.Address.Street == street || c.Address.BuildingNumber == buildingNumber || c.Address.City == city);

        }
    }
}
