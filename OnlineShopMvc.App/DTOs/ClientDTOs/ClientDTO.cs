using System;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShopMvc.App.DTOs.AdressDTOs;

namespace OnlineShopMvc.App.DTOs.ClientDTOs
{
    public class ClientDTO : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AddressDTO Adress { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(s => s.Surname))
                 .ForMember(x => x.Adress, opt => opt.MapFrom(s => s.Address));
        }
    }
}
