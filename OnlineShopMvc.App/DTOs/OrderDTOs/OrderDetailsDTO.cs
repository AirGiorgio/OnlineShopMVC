﻿using AutoMapper;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.OrderProductDTOs;
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
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public ClientDTO Client { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsDTO>()
               .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(x => x.OrderDate, opt => opt.MapFrom(s => s.OrderDate))
               .ForMember(x => x.TotalCost, opt => opt.MapFrom(s => s.TotalCost))
               .ForMember(x => x.OrderProducts, opt => opt.MapFrom(s => s.OrderProducts))
               .ForMember(x => x.Client, opt => opt.MapFrom(s => s.Client));
        }

    }
}
