using OnlineShopMvc.Domain.Model;

namespace OnlineShopMVC.Domain.Model
{
    public class Tag : NamedEntity
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
