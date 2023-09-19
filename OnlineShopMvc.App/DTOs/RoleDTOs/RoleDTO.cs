using AutoMapper;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Domain.Model;

namespace OnlineShopMvc.App.DTOs.RoleDTOs
{
    public class RoleDTO : IMapFrom<Role>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ReverseMap();
        }
    }
}