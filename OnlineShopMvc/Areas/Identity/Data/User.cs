using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        public int? ClientId { get; set;}
        public virtual Client? Client { get; set; }

        public User()
        {
            
        }   
    }
}
