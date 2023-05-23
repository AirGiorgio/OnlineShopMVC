using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ICategoryService
    {
        CategoriesProductsDTO GetCategoryProducts(int id);
        CategoryDTO GetCategoryById(int id);      
        CategoriesForListDTO GetAllCategories(int pagesize, int pageno, string? name);        
        bool UpdateCategory(int id, string? name);
        bool RemoveCategory(int id);
        string AddCategory(string? name);

    }
}
