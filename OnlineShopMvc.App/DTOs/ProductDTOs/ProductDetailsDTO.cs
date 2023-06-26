using OnlineShopMvc.App.Mapping;
using AutoMapper;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using System.ComponentModel.DataAnnotations;
using OnlineShopMvc.Domain.Model;
using FluentValidation;
using System.Text.RegularExpressions;

namespace OnlineShopMvc.App.DTOs.ProductDTOs
{
    public class ProductDetailsDTO : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductCategory { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<int> ProductTags { get; set; }
        public List<TagDTO> Tags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(x => x.Categories, opt => opt.Ignore())
                .ForMember(x => x.Tags, opt => opt.Ignore())
                .ForMember(x => x.ProductTags, opt => opt.Ignore())
                .ForMember(x => x.ProductCategory, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(x => x.IsActive, opt => opt.Ignore());
        }
    }
    public class ProductValidation : AbstractValidator<ProductDetailsDTO>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1);
            RuleFor(x => x.Quantity).Must(quantity => quantity >= 0);
        }
    }
}
