using AutoMapper;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.UserDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.ClientDTOs;

public class ClientDTO : IMapFrom<Client>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsActive { get; set; }
    public AddressDTO Adress { get; set; }
    public UserDTO User { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(x => x.Surname, opt => opt.MapFrom(s => s.Surname))
             .ForMember(x => x.IsActive, opt => opt.MapFrom(s => s.IsActive))
             .ForMember(x => x.Adress, opt => opt.MapFrom(s => s.Address))
             .ForMember(x => x.User, opt => opt.MapFrom(s => s.User));
    }
}