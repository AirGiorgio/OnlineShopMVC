using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IOrderService
    {
        public OrderDetailsDTO GetOrderById(int id);

        public OrdersForListDTO GetAllClientOrders(int id, int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value);

        public OrdersForListDTO GetOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value);

        public bool RemoveOrder(int id);

        public bool AddOrder(int id, List<ProductDTO> orderProducts);
    }
}