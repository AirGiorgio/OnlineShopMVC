using OnlineShopMvc.App.DTOs.ProductDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IProductService
    {
        public ProductDetailsDTO PrepareModel();
        ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? searchCategory, List<int> searchTags, decimal? min, decimal? max, string? name);
        ProductDetailsDTO GetProductById(int id);    
        string AddProduct(ProductDetailsDTO product);
        string UpdateProduct(ProductDetailsDTO product);
        bool RemoveProduct(int id);
    }
}
