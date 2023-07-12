using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.RoleDTOs
{
    public class RoleDTO : IMapFrom<IdentityRole>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityRole, RoleDTO>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.NormalizedName, opt => opt.MapFrom(s => s.NormalizedName));
        }
    }
}
