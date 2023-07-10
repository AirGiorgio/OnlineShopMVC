using OnlineShopMvc.Domain.Model;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IProductRepo
    {
        IQueryable GetProductsFromValue(decimal? min, decimal? max);

        IQueryable GetAllProducts();

        Product GetProductById(int id);

        IQueryable GetProductsFromTags(List<int> tags);

        IQueryable GetProductByName(string name);

        IQueryable GetProductsByCategory(int id);

        string UpdateProduct(Product product);

        bool RemoveProduct(int id);

        bool IsProductNameTaken(string? name);

        bool IsProductNameTaken(string? name, int id);

        string AddProduct(Product product);
    }
}