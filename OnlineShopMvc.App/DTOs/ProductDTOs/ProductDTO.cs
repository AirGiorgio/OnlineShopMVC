using AutoMapper;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Domain.Model;

namespace OnlineShopMvc.App.DTOs.ProductDTOs
{
    public class ProductDTO : IMapFrom<Product>
    {
        public int Id { get; set; }
        public CategoryDTO Category { get; set; }
        public List<TagDTO> Tags { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Category, opt => opt.MapFrom(s => s.Category))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(x => x.Tags, opt => opt.MapFrom(s => s.Tags))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(s => s.Quantity));
        }
        
    }
}
