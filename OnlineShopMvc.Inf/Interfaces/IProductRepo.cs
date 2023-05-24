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
        IQueryable GetProductsByOrderId(int id);
        Product GetProductById(int id);
        IQueryable GetProductsFromTags(List<Tag> tags);
        IQueryable GetProductByName(string name);
        IQueryable GetProductsByCategory(int id);
        bool UpdateProductAmount(int id, int quantity);
        bool UpdateProduct(int id, string name, decimal price, int categoryId, List<Tag> tags);
        bool RemoveProduct(int id);
        string AddProduct(Product product);

    }
}
