using OnlineShopMvc.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace OnlineShopMVC.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public decimal TotalCost { get; set; } 
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var product in OrderProducts)
            {
                totalPrice += product.Product.Price;
            }

            return totalPrice;
        }

    }
}
