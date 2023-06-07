using System;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;

namespace OnlineShopMvc.App.DTOs.ProductDTOs
{
    public class ProductDetailsDTO : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public CategoryDTO ProductCategory { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<TagDTO> ProductTags { get; set; }
        public List<TagDTO> Tags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(x => x.ProductTags, opt => opt.MapFrom(s => s.Tags))
                .ForMember(x=>x.Quantity, opt=>opt.MapFrom(s => s.Quantity))
                .ForMember(x => x.ProductCategory, opt => opt.MapFrom(s => s.Category));
        }
    }
}
