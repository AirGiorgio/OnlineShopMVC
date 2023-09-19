using AutoMapper;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class AdminDTO : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AdminDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName));
        }
    }
}