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
        Order GetOrderById(int? id, Client? client);
        Order GetOrderById(int? id);
        IQueryable GetOrdersFromDate(DateTime? orderDate, Client? client);
        IQueryable GetAllOrdersFromDate(DateTime? orderDate);
        IQueryable GetOrdersByOrderDate(Client? client);
        IQueryable GetOrdersByOrderDate();
        IQueryable GetOrdersFromValue(Client? client, int? min, int? max);
        IQueryable GetOrdersFromValue(int? min, int? max);
        IQueryable GetOrdersByValue(Client? client);
        IQueryable GetOrdersByValue();
        bool RemoveOrder(int? id);
        bool AddOrder(Client client, List<Product> orderProducts);

    }
}
