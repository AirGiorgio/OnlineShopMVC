using AutoMapper;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.OrderDTOs
{
    public class OrderDetailsDTO : IMapFrom<Order>
    {
        public List<ProductDTO> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsDTO>()
                .ForMember(x => x.Products, opt => opt.MapFrom(s => s.Products));
               
        }

    }
}
