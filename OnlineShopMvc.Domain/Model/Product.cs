using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Domain.Model
{
    public class Product : NamedEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}