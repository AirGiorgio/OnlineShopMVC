using OnlineShopMvc.Domain.Model;

namespace OnlineShopMVC.Domain.Model
{
    public class Order :BaseEntity
    {
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public decimal TotalCost { get; set; } 
      

    }
}
