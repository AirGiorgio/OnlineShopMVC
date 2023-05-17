using OnlineShopMvc.App.DTOs.CategoryDTOs;
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
        ProductsForListDTO GetAllProducts(); 
        ProductDetailsDTO GetProductById(int id); 
        ProductsForListDTO GetProductsFromTags(List<Tag>? tags); 
        ProductDTO GetProductByName(string? name); 
        ProductsForListDTO GetProductsByCategory(Category? category); 
        bool UpdateProduct(Product? product, string? name, string? price, string? quantity, Category? category, List<Tag>? tags);
        bool UpdateProductAmount(Product? product, string? quantity);  
        bool RemoveProduct(int id);
        string AddProduct(string? name, string? price, string? quantity, Category? category, List<Tag>? tags);
      

    }
}
