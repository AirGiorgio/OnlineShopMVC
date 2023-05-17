using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.CategoryDTOs
{
    public class CategoriesForListDTO : IMapFrom<Category>
    {
        public List<CategoryDTO> Categories { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }
    }
}
