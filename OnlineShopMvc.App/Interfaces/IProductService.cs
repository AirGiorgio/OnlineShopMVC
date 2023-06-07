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
        ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? categoryId, List<int> searchTags, decimal? min, decimal? max, string? name); 
        ProductDetailsDTO GetProductById(int id);    
        string AddProduct(int? amount, string? name, string? price, int categoryId, List<Tag>? tags);
        string UpdateProduct(int id, int? amount, string? name, string? price, int categoryId, List<Tag>? tags);
        bool RemoveProduct(int id);
    }
}
