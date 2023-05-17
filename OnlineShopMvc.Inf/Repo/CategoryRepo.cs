using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return context.Categories.SingleOrDefault(i => i.Id == id);
        }
        public Category GetCategoryByName(string name)
        {
            return context.Categories.Where(i => i.Name.StartsWith(name)).SingleOrDefault();
        }

        public IQueryable GetAllCategories(int pagesize, int pageno)
        {
            return context.Categories;
        }
        public bool UpdateCategory(string c)
        {
            var category = GetCategoryByName(c);
            if (category != null)
            {
                category.Name = c;
                context.Update(c);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool RemoveCategory(int? id)
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

        public string AddCategory(string name)
        {
            Category category = new Category();
            category.Name=name;

            context.Add(category);
            context.SaveChanges();
            return category.Name; 
        }
    }
}
