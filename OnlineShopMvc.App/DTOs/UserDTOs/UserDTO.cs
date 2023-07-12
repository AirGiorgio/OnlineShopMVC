using Microsoft.AspNetCore.Identity;
using OnlineShopMvc.App.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class UserDTO : IMapFrom<IdentityUser>
    {
    }
}
