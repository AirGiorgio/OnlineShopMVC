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
        ProductsForListDTO GetProductsFromValue(decimal? min, decimal? max);
        ProductsForListDTO GetAllProducts(); 
        ProductDetailsDTO GetProductById(int id); 
        ProductsForListDTO GetProductsFromTags(List<Tag>? tags);
        ProductsForListDTO GetProductByName(string? name);
        ProductsForListDTO GetProductsByCategory(int id); 
        bool UpdateProduct(int id, string? name, string? price, int categoryId, List<Tag>? tags);
        bool UpdateProductAmount(int id, string? quantity);  
        bool RemoveProduct(int id);
        string AddProduct(string? name, string? price, string? quantity, int categoryId, List<Tag>? tags);
      

    }
}
