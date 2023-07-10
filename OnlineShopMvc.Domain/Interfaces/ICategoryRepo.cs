using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface ICategoryRepo
    {
        Category GetCategoryById(int? id);

        IQueryable GetAllCategories(string? name);

        string UpdateCategory(int id, string c);

        bool RemoveCategory(int? id);

        bool IsCategoryNameTaken(string? name);

        string AddCategory(string name);
    }
}