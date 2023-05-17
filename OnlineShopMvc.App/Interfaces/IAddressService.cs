using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IAddressService
    {
        AddressDTO GetAddressByClientId(int id); 

       ClientsForListDTO GetClientByStreetDetails(string? street, string? buildingNumber, string? city);

    }
}
