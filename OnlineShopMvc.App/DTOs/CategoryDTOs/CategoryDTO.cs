using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.CategoryDTOs
{
    public class CategoryDTO : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDTO>()
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
   
}
