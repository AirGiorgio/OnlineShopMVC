using OnlineShopMvc.App.DTOs.ProductDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IProductService
    {
        public ProductDetailsDTO PrepareModel();

        public ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? searchCategory, List<int> searchTags, decimal? min, decimal? max, string? name);

        public ProductDetailsDTO GetProductById(int id);

        public string AddProduct(ProductDetailsDTO product);

        public string UpdateProduct(ProductDetailsDTO product);

        public bool RemoveProduct(int id);
    }
}