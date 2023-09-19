using Microsoft.AspNetCore.Identity;
using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.Domain.Model
{
    public class Role : IdentityRole
    {
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
        }
    }
}