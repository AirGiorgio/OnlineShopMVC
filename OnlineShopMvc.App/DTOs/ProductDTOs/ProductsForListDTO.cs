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
    public class ProductsForListDTO : IMapFrom<Product>
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<TagDTO> Tags { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }
    }
}
