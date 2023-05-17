using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IOrderService
    {
        OrderDetailsDTO GetOrderById(int id); 
        OrdersForListDTO GetAllOrdersFromDate(DateTime? orderDate);  
        OrdersForListDTO GetOrdersFromDate(DateTime? orderDate, Client? client); 
        OrdersForListDTO GetOrdersByOrderDate(); 
        OrdersForListDTO GetOrdersByOrderDate(Client? client); 
        OrdersForListDTO GetOrdersFromValue(int? min, int? max);
        OrdersForListDTO GetOrdersFromValue(Client? client, int? min, int? max); 
        OrdersForListDTO GetOrdersByValue(); 
        OrdersForListDTO GetOrdersByValue(Client? client);  
        bool RemoveOrder(int id);
        bool AddOrder(Client? client, List<Product> orderProducts); 
    }
}
