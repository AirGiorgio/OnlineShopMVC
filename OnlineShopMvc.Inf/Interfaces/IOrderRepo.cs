using OnlineShopMvc.Domain.Model;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IOrderRepo
    {
        Order GetClientOrderById(int? orderId, int clientId);
        Order GetOrderById(int? id);
        IQueryable GetClientOrdersFromDate(DateTime? orderDate, int id);
        IQueryable GetAllOrdersFromDate(DateTime? orderDate);
        IQueryable GetClientOrdersByOrderDate(int id);
        IQueryable GetOrdersByOrderDate();
        IQueryable GetClientOrdersFromValue(int id, decimal? min, decimal? max);
        IQueryable GetOrdersFromValue(decimal? min, decimal? max);
        IQueryable GetClientOrdersByValue(int id);
        IQueryable GetOrdersByValue();
        bool RemoveOrder(int? id);
        bool AddOrder(int id, List<Product> orderProducts);

    }
}
