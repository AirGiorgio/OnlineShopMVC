using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Domain.Model;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.Services
{
    public class ProductServices : IProductService
    {
        private readonly ITagRepo _tagRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        public ProductServices(IProductRepo productRepo, IMapper mapper, ITagRepo tagRepo, ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
        }

        public ProductDetailsDTO PrepareModel()
        {
            ProductDetailsDTO newProductDTO = new ProductDetailsDTO();
            newProductDTO.Categories = _categoryRepo.GetAllCategories(null).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            newProductDTO.Tags = _tagRepo.GetAllTags(null).ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();
            return newProductDTO;
        }
        public ProductDetailsDTO GetProductById(int id)
        {
            if (id <= 0 || id==null)
            {
                return null;
            }
            else
            {
                var product = _productRepo.GetProductById(id);
                var productDTO = _mapper.Map<ProductDetailsDTO>(product);     
                var categories = _categoryRepo.GetAllCategories(null)
                    .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
                var tags = _tagRepo.GetAllTags(null).ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();
                productDTO.Categories = categories;
                productDTO.ProductCategory = (product.CategoryId.HasValue==true ? product.CategoryId.Value : 0);
                productDTO.ProductTags = product.Tags.Select(x=>x.Id).ToList();
                productDTO.Tags = tags;

                return productDTO;
            }
        }
        public bool RemoveProduct(int id)
        {
            if (id <= 0 || id == null)
            {
                return false;
            }
            return _productRepo.RemoveProduct(id);
        }

        public ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? searchCategory, List<int> searchTags, decimal? min, decimal? max, string? name)
        {
            ProductsForListDTO productsForListDTO = new ProductsForListDTO();
            var products = new List<ProductDTO>();
            
            var categories = _categoryRepo.GetAllCategories(null)
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();

            var tags = _tagRepo.GetAllTags(null)
            .ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();

            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (searchCategory==null && !min.HasValue && !max.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetAllProducts()
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();

            }
            else if (searchCategory.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetProductsByCategory(searchCategory.Value)
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                productsForListDTO.SearchCategory = _mapper.Map<CategoryDTO>(_categoryRepo.GetCategoryById(searchCategory));  
            }
            else if (min.HasValue || max.HasValue)
            {
                products = _productRepo.GetProductsFromValue(min, max)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (!name.IsNullOrEmpty())
            {
                products = _productRepo.GetProductByName(name)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (searchTags.Count > 0)
            {
                products = _productRepo.GetProductsFromTags(searchTags)
                   .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();

                List<TagDTO> sTagsDto = new List<TagDTO>();
                foreach (var item in searchTags)
                {
                    sTagsDto.Add(_mapper.Map<TagDTO>(_tagRepo.GetTagById(item)));
                }
                productsForListDTO.SearchTags = sTagsDto;
            }
            var productsToShow = products.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();

            productsForListDTO.Products = productsToShow;
            productsForListDTO.PageNum = pageNo.Value;
            productsForListDTO.MinValue = min;
            productsForListDTO.MaxValue = max;
            productsForListDTO.SearchString = name;
            productsForListDTO.Categories = categories;
            productsForListDTO.Tags = tags;
            productsForListDTO.PageSize = pageSize.Value;
            productsForListDTO.Count = products.Count;
            
            return productsForListDTO;
        }
        public string UpdateProduct(ProductDetailsDTO product)
        {
            if (product.Id<=0 || product.Id==null)
            {
                return null;
            }
            if (product.Name == null || product.Price == null || product.ProductCategory ==null || product.Quantity == null || product.ProductTags.IsNullOrEmpty())
            {
                return "Nieprawidłowe dane produktu";
            }
            else if (product.Price<=0 || product.Quantity<=0)
            {
                return "Nieprawidłowa cena produktu";
            }
            else if (_productRepo.IsProductNameTaken(product.Name, product.Id))
            {
                return "Istnieje już taki produkt";
            }
            else
            {
                Category category = _categoryRepo.GetCategoryById(product.ProductCategory);
                var tagsIds = new List<Tag>();
                foreach (var item in product.ProductTags)
                {
                    tagsIds.Add(_tagRepo.GetTagById(item));
                }
                var p = _mapper.Map<Product>(product);
                p.Category= category;
                p.Tags = tagsIds;
                p.IsActive = true;
                return _productRepo.UpdateProduct(p);
            }
        }
        public string AddProduct(ProductDetailsDTO product)
        {
            if (product.Name == null || product.Price == null || product.ProductCategory == null || product.Quantity <= 0 || product.ProductTags.IsNullOrEmpty())
            {
                return "Nieprawidłowe dane produktu";
            }
            else if (product.Price <= 0)
            {
                return "Nieprawidłowa cena produktu";
            }
            else if (_productRepo.IsProductNameTaken(product.Name))
            {
                return "Istnieje już taki produkt";
            }
            else
            {
                Category category = _categoryRepo.GetCategoryById(product.ProductCategory);
                var tagsIds = new List<Tag>();
                foreach (var item in product.ProductTags)
                {
                    tagsIds.Add(_tagRepo.GetTagById(item));
                }
                var p = _mapper.Map<Product>(product);
                p.Category = category;
                p.Tags = tagsIds;
                p.IsActive = true;
                return _productRepo.AddProduct(p);
            }
               
        }
    }
}
