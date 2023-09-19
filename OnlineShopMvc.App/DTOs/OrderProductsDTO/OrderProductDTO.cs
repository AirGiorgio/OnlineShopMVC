using AutoMapper;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Domain.Model;

namespace OnlineShopMvc.App.DTOs.OrderProductsDTO
{
    public class OrderProductDTO : IMapFrom<OrderProduct>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual OrderDTO Order { get; set; }
        public int Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderProduct, OrderProductDTO>()
               .ForMember(x => x.OrderId, opt => opt.MapFrom(s => s.OrderId))
               .ForMember(x => x.ProductId, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(x => x.Product, opt => opt.MapFrom(s => s.Product))
               .ForMember(x => x.Order, opt => opt.MapFrom(s => s.Order))
               .ForMember(x => x.Amount, opt => opt.MapFrom(s => s.Amount))
               .ReverseMap();
        }
    }
}