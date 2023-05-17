using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Context context;
        public OrderRepo(Context context)
        {
            this.context = context;
        }
        public Order GetOrderById(int? id, Client? client)
        {
            return context.Orders.SingleOrDefault(i => i.Id == id && i.ClientId == client.Id);
        }
        public Order GetOrderById(int? id)
        {
            return context.Orders.SingleOrDefault(i => i.Id == id);
        }

        public IQueryable GetOrdersFromDate(DateTime? orderDate, Client? client)  // od konkretnej daty
        {
            return context.Orders.Where(x => x.OrderDate > orderDate && x.ClientId == client.Id);
                                           
        }
        public IQueryable GetAllOrdersFromDate(DateTime? orderDate)  // od konkretnej daty
        {
            return context.Orders.Where(x => x.OrderDate > orderDate);
          
        }
        public IQueryable GetOrdersByOrderDate(Client? client) //od początku do końca
        {
            return context.Orders.Where(x => x.ClientId == client.Id).OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersByOrderDate() //od początku do końca
        {
            return context.Orders.OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersFromValue(Client? client, int? min, int? max) //w konkretnych przedziałach
        {
            return context.Orders.Where(x => x.GetTotalPrice() > min && x.GetTotalPrice() < max && x.ClientId == client.Id);
        }
        public IQueryable GetOrdersFromValue(int? min, int? max) //w konkretnych przedziałach
        {
            return context.Orders.Where(x => x.GetTotalPrice() > min && x.GetTotalPrice() < max);
        }

        public IQueryable GetOrdersByValue(Client? client) 
        {
            return context.Orders.Where(x => x.ClientId == client.Id).OrderBy(x => x.GetTotalPrice());
        }
        public IQueryable GetOrdersByValue()
        {
            return context.Orders.OrderBy(x => x.GetTotalPrice());
        }

        public bool RemoveOrder(int? id) 
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                context.Remove(order);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool AddOrder(Client client, List<Product> orderProducts)
        {
            Order order = new Order();
            order.ClientId = client.Id;
            order.OrderDate = DateTime.Now;
            order.Products = orderProducts;
            order.TotalCost = order.GetTotalPrice();
            client.OrderHistory.Add(order);

            context.Add(order);
            context.SaveChanges();
            return true;            
        }
      
    }
}
