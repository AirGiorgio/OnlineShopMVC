using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IAddressRepo
    {
        Address GetAddressByClientId(int id);
        IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city);
    }
}
