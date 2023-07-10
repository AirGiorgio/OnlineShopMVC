using OnlineShopMvc.App.DTOs.CategoryDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ICategoryService
    {
        public CategoryDTO PrepareModel();

        CategoriesProductsDTO GetCategoryProducts(int id);

        CategoryDTO GetCategoryById(int id);

        CategoriesForListDTO GetAllCategories(int? pageSize, int? pageNo, string? name);

        string UpdateCategory(int id, string? name);

        bool RemoveCategory(int id);

        string AddCategory(string? name);
    }
}