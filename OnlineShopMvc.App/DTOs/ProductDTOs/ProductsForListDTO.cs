using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.ProductDTOs
{
    public class ProductsForListDTO
    {
        public List<ProductDTO> Products { get; set; }
        public int Count { get; set; }
    }
}
