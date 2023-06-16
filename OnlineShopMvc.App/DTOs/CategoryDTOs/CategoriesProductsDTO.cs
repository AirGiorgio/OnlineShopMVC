using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.CategoryDTOs
{
    public class CategoriesProductsDTO : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDTO> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoriesProductsDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Products, opt => opt.MapFrom(s => s.Products));
        }
    }
   
}
