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
using FluentValidation;

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
                .ForMember(x => x.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(x => x.ProductCategory, opt => opt.MapFrom(s => s.Category))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ProductTags))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ProductCategory));
        }
      
    }
    public class ProductValidation : AbstractValidator<ProductDetailsDTO>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1);
            RuleFor(x => x.Price).ScalePrecision(18,2);
            RuleFor(x => x.ProductTags).NotNull();
            RuleFor(x => x.ProductCategory).NotNull();
            RuleFor(x => x.Quantity).Must(quantity => quantity % 1 == 0);
        }
    }
}
