using AutoMapper;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.ClientDTOs
{
    public class ClientOrdersDTO : IMapFrom<Client>
    {
        public List<OrderDTO> Orders { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public int? SortByPrice { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientOrdersDTO>()
                .ForMember(x => x.Orders, opt => opt.MapFrom(s => s.OrderHistory));
        }
    }
}