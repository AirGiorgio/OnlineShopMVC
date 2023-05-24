using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Domain.Model
{
    public class ProductTag
    {
        public int ProductId { get; set; }

        public virtual ICollection<Product> TagProducts { get; set; }
        public int TagId { get; set; }

        public virtual ICollection<Tag> ProductTags { get; set; }
    }
}
