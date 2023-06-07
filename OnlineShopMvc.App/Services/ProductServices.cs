using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? categoryId, List<int> searchTags, decimal? min, decimal? max, string? name)
        {
            List<ProductDTO> products = new List<ProductDTO>();
            
            var productsDTO = new ProductsForListDTO();
            productsDTO.SearchTags = new List<TagDTO>();

            var categories = _categoryRepo.GetAllCategories(null)
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();

            var tags = _tagRepo.GetAllTags(null)
            .ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();

            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (!categoryId.HasValue && !min.HasValue && !max.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetAllProducts()
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();

            }
            else if (!min.HasValue && !max.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetProductsByCategory(categoryId.Value)
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                productsDTO.SearchCategory = (_mapper.Map<CategoryDTO>(_categoryRepo.GetCategoryById(categoryId.Value)));  
            }
            else if (min.HasValue || max.HasValue)
            {
                if (min.HasValue && !max.HasValue)
                {
                    max = 100000;
                }
                else if (max.HasValue && !min.HasValue)
                {
                    min = 0;
                }
                products = _productRepo.GetProductsFromValue(min.Value, max.Value)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (!name.IsNullOrEmpty())
            {
                products = _productRepo.GetProductByName(name)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (searchTags.Count > 0)
            {
                List<int> SearchTags = searchTags;
                products = _productRepo.GetProductsFromTags(SearchTags)
                   .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();

                foreach (int item in SearchTags)
                {
                    productsDTO.SearchTags.Add(_mapper.Map<TagDTO>(_tagRepo.GetTagById(item)));
                }
           
            }
            var productsToShow = products.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();

            productsDTO.Products = productsToShow;
            productsDTO.PageNum = pageNo.Value;
            productsDTO.MinValue = min;
            productsDTO.MaxValue = max;
            productsDTO.SearchString = name;
            productsDTO.Categories = categories;
            productsDTO.Tags = tags;
            productsDTO.PageSize = pageSize.Value;
            productsDTO.Count = products.Count;
            
            return productsDTO;

        }
        public string UpdateProduct(int id, int? amount, string? name, string? price, int categoryId, List<Tag>? tags)
        {
            if (id<=0 || id==null)
            {
                return null;
            }
            if (name == null || price == null || categoryId <= 0 || amount <= 0 || tags.IsNullOrEmpty())
            {
                return "Nieprawidłowe dane produktu";
            }
            else if (!decimal.TryParse(price, out decimal pric))
            {
                return "Nieprawidłowa cena produktu";
            }
            else if (_productRepo.GetProductByName(name) != null)
            {
                return "Istnieje już taki produkt";
            }
            else return _productRepo.UpdateProduct(id, amount.Value, name, pric, categoryId, tags);
        }


        public string AddProduct(int? amount, string? name, string? price, int categoryId, List<Tag>? tags)
        {

            if (name == null || price == null || categoryId <= 0 || amount <= 0 || tags.IsNullOrEmpty())
            {
                return "Nieprawidłowe dane produktu";
            }
            else if (!decimal.TryParse(price, out decimal pric))
            {
                return "Nieprawidłowa cena produktu";
            }
            else if (_productRepo.GetProductByName(name) != null)
            {
                return "Istnieje już taki produkt";
            }
            else return _productRepo.AddProduct(amount.Value, name, pric, categoryId, tags);
        }
    }
}
