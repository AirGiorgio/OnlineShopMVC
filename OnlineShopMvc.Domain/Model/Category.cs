using OnlineShopMvc.Domain.Model;

namespace OnlineShopMVC.Domain.Model
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
