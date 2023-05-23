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
        OrdersForListDTO GetOrdersFromDate(DateTime? orderDate, int id); 
        OrdersForListDTO GetOrdersByOrderDate(); 
        OrdersForListDTO GetOrdersByOrderDate(int id); 
        OrdersForListDTO GetOrdersFromValue(decimal? min, decimal? max);
        OrdersForListDTO GetOrdersFromValue(int id, decimal? min, decimal? max); 
        OrdersForListDTO GetOrdersByValue(); 
        OrdersForListDTO GetOrdersByValue(int id);  
        bool RemoveOrder(int id);
        bool AddOrder(int id, List<Product> orderProducts); 
    }
}
