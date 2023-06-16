using AutoMapper;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.ProductDTOs
{
    public class ProductsForListDTO 
    {
        public List<ProductDTO> Cart { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; } 
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public CategoryDTO SearchCategory { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<TagDTO> SearchTags { get; set; }
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }


    }
}
