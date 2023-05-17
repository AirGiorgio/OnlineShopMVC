using Azure;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface ICategoryRepo
    {
        Category GetCategoryById(int? id);
        Category GetCategoryByName(string name);
        IQueryable GetAllCategories(int pagesize, int pageno);
        bool UpdateCategory(string c);
        bool RemoveCategory(int? id);
        string AddCategory(string name);
    }
}
