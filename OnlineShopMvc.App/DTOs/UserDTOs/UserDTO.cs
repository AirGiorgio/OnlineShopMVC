using AutoMapper;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class UserDTO : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int ClientId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(x => x.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(x => x.ClientId, opt => opt.MapFrom(s => s.ClientId));
        }
    }
}