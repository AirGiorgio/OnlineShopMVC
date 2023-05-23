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
        Order GetOrderById(int? orderId, int clientId);
        Order GetOrderById(int? id);
        IQueryable GetOrdersFromDate(DateTime? orderDate, int id);
        IQueryable GetAllOrdersFromDate(DateTime? orderDate);
        IQueryable GetOrdersByOrderDate(int id);
        IQueryable GetOrdersByOrderDate();
        IQueryable GetOrdersFromValue(int id, decimal min, decimal max);
        IQueryable GetOrdersFromValue(decimal min, decimal max);
        IQueryable GetOrdersByValue(int id);
        IQueryable GetOrdersByValue();
        bool RemoveOrder(int? id);
        bool AddOrder(int id, List<Product> orderProducts);

    }
}
