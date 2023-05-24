using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Domain.Model
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public virtual ICollection<Order> ProductOrders{ get; set; }
        public int ProductId { get; set; }

        public virtual ICollection<Product> OrderProducts { get; set; }
    }
}
