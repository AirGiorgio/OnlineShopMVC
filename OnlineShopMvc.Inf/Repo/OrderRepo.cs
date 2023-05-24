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
        public Order GetOrderById(int? orderId, int clientId)
        {
            return context.Orders.SingleOrDefault(i => i.Id == orderId && i.ClientId == clientId);
        }
        public Order GetOrderById(int? id)
        {
            return context.Orders.SingleOrDefault(i => i.Id == id);
        }

        public IQueryable GetOrdersFromDate(DateTime? orderDate, int id)  // od konkretnej daty dla klienta
        {
            return context.Orders.Where(x => x.OrderDate > orderDate && x.ClientId == id).OrderBy(x => x.OrderDate);
                                           
        }
        public IQueryable GetAllOrdersFromDate(DateTime? orderDate)   
        {
            return context.Orders.Where(x => x.OrderDate > orderDate).OrderBy(x=>x.OrderDate);
          
        }
        public IQueryable GetOrdersByOrderDate(int id) //od początku do końca dla klienta
        {
            return context.Orders.Where(x => x.ClientId == id).OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersByOrderDate()   
        {
            return context.Orders.OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersFromValue(int id, decimal min, decimal max) //w konkretnych przedziałach dla klienta
        {
            return context.Orders.Where(x => x.TotalCost > min && x.TotalCost < max && x.ClientId == id).OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersFromValue(decimal min, decimal max)  
        {
            return context.Orders.Where(x => x.TotalCost > min && x.TotalCost < max).OrderBy(x=>x.OrderDate);
        }

        public IQueryable GetOrdersByValue(int id) 
        {
            return context.Orders.Where(x => x.ClientId == id).OrderBy(x => x.TotalCost);
        }
        public IQueryable GetOrdersByValue()
        {
            return context.Orders.OrderBy(x => x.TotalCost);
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

        public bool AddOrder(int id, List<Product> orderProducts)
        {
            //Order order = new Order();
            //order.ClientId = id;
            //order.OrderDate = DateTime.Now;
            //order.Products = orderProducts;
            //order.TotalCost = order.GetTotalPrice();
            ////client.OrderHistory.Add(order);

            //context.Add(order);
            //context.SaveChanges();
            return true;            
        }
      
    }
}
