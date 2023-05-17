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
        IQueryable GetAllProducts();
        Product GetProductById(int? id);
        IQueryable GetProductsFromTags(List<Tag> tags);
        Product GetProductByName(string name);
        IQueryable GetProductsByCategory(Category category);
        bool UpdateProductAmount(Product? product, int quantity);
        bool UpdateProduct(Product product);
        bool RemoveProduct(int? id);
        string AddProduct(Product product);

    }
}
