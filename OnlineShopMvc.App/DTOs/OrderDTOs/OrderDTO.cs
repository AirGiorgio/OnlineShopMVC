using AutoMapper;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.OrderDTOs
{
    public class OrderDTO : IMapFrom<Order>
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDTO>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.OrderDate, opt => opt.MapFrom(s => s.OrderDate))
                .ForMember(x => x.TotalCost, opt => opt.MapFrom(s => s.TotalCost));
        }
    }
}