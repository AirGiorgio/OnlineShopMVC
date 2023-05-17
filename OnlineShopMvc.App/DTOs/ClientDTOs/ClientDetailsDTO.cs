using AutoMapper;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.ClientDTOs
{
    public class ClientDetailsDTO : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAdress { get; set; }
        public string Telephone { get; set; }
        public AddressDTO Adress { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDetailsDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(s => s.Surname))
                .ForMember(x => x.EmailAdress, opt => opt.MapFrom(s => s.EmailAdress))
                .ForMember(x => x.Telephone, opt => opt.MapFrom(s => s.Telephone))
                .ForMember(x => x.Adress, opt => opt.MapFrom(s => s.Address));

        }
    }
}
