using System;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.ClientDTOs
{
    public class ClientsForListDTO : IMapFrom<Client>
    {
        public List<ClientDTO> Clients { get; set; }
        public int Count { get; set; }
    }
}
