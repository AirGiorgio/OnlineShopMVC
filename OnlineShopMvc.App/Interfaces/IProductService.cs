using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IProductService
    {
        public ProductDetailsDTO PrepareModel();
        ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, CategoryDTO searchCategory, List<TagDTO> searchTags, decimal? min, decimal? max, string? name);
        ProductDetailsDTO GetProductById(int id);    
        string AddProduct(ProductDetailsDTO product);
        string UpdateProduct(ProductDetailsDTO product);
        bool RemoveProduct(int id);
    }
}
