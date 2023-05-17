using AutoMapper;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.AdressDTOs
{
    public class AddressDTO : IMapFrom<Address>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

        public string FlatNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Street, opt => opt.MapFrom(s => s.Street))
                .ForMember(x => x.BuildingNumber, opt => opt.MapFrom(s => s.BuildingNumber))
                .ForMember(x => x.FlatNumber, opt => opt.MapFrom(s => s.FlatNumber))
                .ForMember(x => x.City, opt => opt.MapFrom(s => s.City))
                .ForMember(x => x.ZipCode, opt => opt.MapFrom(s => s.ZipCode));
        }
    }
}
