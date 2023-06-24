using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IOrderService
    {
        OrderDetailsDTO GetOrderById(int id); 
        OrdersForListDTO GetAllClientOrders(int id, int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value);  
        OrdersForListDTO GetOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value); 
        bool RemoveOrder(int id);
        bool AddOrder(int id, List<ProductDTO> orderProducts); 
    }
}
