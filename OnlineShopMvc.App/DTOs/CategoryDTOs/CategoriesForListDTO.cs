using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.CategoryDTOs
{
    public class CategoriesForListDTO 
    {
        public List<CategoryDTO> Categories { get; set; }
        public CategoryDTO NewCategory { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
