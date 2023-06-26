using Microsoft.EntityFrameworkCore;
using OnlineShopMvc.Domain.Model;
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
        public Order GetClientOrderById(int? orderId, int clientId)
        {
            return context.Orders.SingleOrDefault(i => i.Id == orderId && i.ClientId == clientId);
        }
        public Order GetOrderById(int? id)
        {
            return context.Orders.Include(x=>x.OrderProducts).ThenInclude(x=>x.Product).Include(x=>x.Client).SingleOrDefault(i => i.Id == id);
        }

        public IQueryable GetClientOrdersFromDate(DateTime? orderDate, int id)  
        {
            return context.Orders.Where(x => x.OrderDate > orderDate && x.ClientId == id).OrderBy(x => x.OrderDate);
                                           
        }
        public IQueryable GetAllOrdersFromDate(DateTime? orderDate)   
        {
            return context.Orders.Where(x => x.OrderDate > orderDate).OrderBy(x=>x.OrderDate);
          
        }
        public IQueryable GetClientOrdersByOrderDate(int id)  
        {
            return context.Orders.Where(x => x.ClientId == id).OrderBy(x => x.OrderDate);
        }
        public IQueryable GetOrdersByOrderDate()   
        {
            return context.Orders.OrderBy(x => x.OrderDate);
        }
        public IQueryable GetClientOrdersFromValue(int id, decimal? min, decimal? max)
        {
            if (min.HasValue && !max.HasValue)
            {
                max = context.Orders.Max(x=>x.TotalCost);
            }
            else if (max.HasValue && !min.HasValue)
            {
                min = 0;
            }
            return context.Orders.Where(x => x.TotalCost > min && x.TotalCost < max && x.ClientId == id).OrderBy(x => x.TotalCost);
        }
        public IQueryable GetOrdersFromValue(decimal? min, decimal? max)
        {
            if (min.HasValue && !max.HasValue)
            {
                max = context.Orders.Max(x => x.TotalCost);
            }
            else if (max.HasValue && !min.HasValue)
            {
                min = 0;
            }
            return context.Orders.Where(x => x.TotalCost > min && x.TotalCost < max).OrderBy(x=>x.TotalCost);
        }
        public IQueryable GetClientOrdersByValue(int id) 
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
