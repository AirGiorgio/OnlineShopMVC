using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.CategoryDTOs;

public class CategoryDTO : IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDTO>().ReverseMap()
            .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name));
    }
}

public class CategoryValidation : AbstractValidator<CategoryDTO>
{
    public CategoryValidation()
    {
        RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1);
    }
}