using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.Inf.Data;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly Context context;

        public CategoryRepo(Context context)
        {
            this.context = context;
        }

        public Category GetCategoryById(int? id)
        {
            return context.Categories.Include(x => x.Products).SingleOrDefault(i => i.Id == id);
        }

        public IQueryable GetAllCategories(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return context.Categories;
            }
            else
            {
                return context.Categories.Where(x => x.Name.StartsWith(name));
            }
        }

        public string UpdateCategory(int id, string c)
        {
            try
            {
                var category = GetCategoryById(id);
                if (category != null)
                {
                    category.Name = c;
                    context.Update(category);
                    context.SaveChanges();
                    return "Uaktualniono kategorię";
                }
                else return "Nie udało się odnaleźć tej kategorii";
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public bool RemoveCategory(int? id)
        {
            try
            {
                var category = GetCategoryById(id);
                if (category != null)
                {
                    context.Remove(category);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string AddCategory(string name)
        {
            try
            {
                Category category = new Category();
                category.Name = name;

                context.Add(category);
                context.SaveChanges();
                return "Udało się dodać kategorię " + category.Name;
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public bool IsCategoryNameTaken(string? name)
        {
            if (context.Categories.Any(x => x.Name == name))
            {
                return true;
            }
            else return false;
        }
    }
}