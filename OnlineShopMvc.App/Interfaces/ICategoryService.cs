using OnlineShopMvc.App.DTOs.CategoryDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ICategoryService
    {
        public CategoriesProductsDTO GetCategoryProducts(int id);

        public CategoryDTO GetCategoryById(int id);

        public CategoriesForListDTO GetAllCategories(int? pageSize, int? pageNo, string? name);

        public string UpdateCategory(int id, string? name);

        public bool RemoveCategory(int id);

        public string AddCategory(string? name);
    }
}