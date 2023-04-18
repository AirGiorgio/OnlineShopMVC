using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMVC.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }

        public int CLientId { get; set; }

        public string ShippingAdress { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<Product> Products { get; set; }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var product in Products)
            {
                totalPrice += product.Price;
            }

            return totalPrice;
        }

    }
}
