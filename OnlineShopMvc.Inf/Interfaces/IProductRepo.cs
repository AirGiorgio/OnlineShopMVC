using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IProductRepo
    {
        IQueryable GetProductsFromValue(decimal min, decimal max);
        IQueryable GetAllProducts();
        Product GetProductById(int id);
        IQueryable GetProductsFromTags(List<int> tags);
        IQueryable GetProductByName(string name);
        IQueryable GetProductsByCategory(int id);
        string UpdateProduct(int id, int amount, string name, decimal price, int categoryId, List<Tag> tags);
        bool RemoveProduct(int id);
        string AddProduct(int amount, string name, decimal price, int categoryId, List<Tag> tags);

    }
   
}
