using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.OrderDTOs
{
    public class OrdersForListDTO : IMapFrom<Order>
    {
        public List<OrderDTO> Orders { get; set; }
        public int Count { get; set; }
    }
}
