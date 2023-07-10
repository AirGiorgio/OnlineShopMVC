using AutoMapper;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderProductsDTO;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.OrderDTOs
{
    public class OrderDetailsDTO : IMapFrom<Order>
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public string OrderId { get; set; }
        public ClientDTO Client { get; set; }

        public List<OrderProductDTO> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsDTO>()
               .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(x => x.OrderDate, opt => opt.MapFrom(s => s.OrderDate))
               .ForMember(x => x.OrderId, opt => opt.MapFrom(s => s.OrderId))
               .ForMember(x => x.TotalCost, opt => opt.MapFrom(s => s.TotalCost))
               .ForMember(x => x.Client, opt => opt.MapFrom(s => s.Client))
               .ForMember(x => x.Products, opt => opt.MapFrom(s => s.OrderProducts))
               .ReverseMap()
               .ForMember(x => x.OrderProducts, opt => opt.MapFrom(s => s.Products));
        }
    }
}