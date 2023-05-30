using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, int? categoryId, List<Tag> tags, decimal? min, decimal? max, string? name)
        {
            List<ProductDTO> products = new List<ProductDTO>();

            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (!categoryId.HasValue && !min.HasValue && !max.HasValue && name.IsNullOrEmpty() && tags.IsNullOrEmpty())
            {
                 products = _productRepo.GetAllProducts()
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                
            }
            else if (!min.HasValue && !max.HasValue && name.IsNullOrEmpty() && tags.IsNullOrEmpty())
            {
                 products = _productRepo.GetProductsByCategory(categoryId.Value)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (min.HasValue || max.HasValue)
            {
                products = _productRepo.GetProductsFromValue(min.Value, max.Value) //min max
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (!name.IsNullOrEmpty())
            {
                products = _productRepo.GetProductByName(name)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (tags.Count > 0)
            {
                 products = _productRepo.GetProductsFromTags(tags)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            }
            var productsToShow = products.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();
            var categories = _categoryRepo.GetAllCategories(null)
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            var tag = _tagRepo.GetAllTags(null)
                .ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();

            var productsDTO = new ProductsForListDTO()
            {
                Products = productsToShow,
                PageNum = pageNo.Value,
                Categories = categories,
                Tags = tag,
                PageSize = pageSize.Value,
                Count = products.Count
            };
            return productsDTO;

        }

        public bool AddOrUpdateProduct(int id, int? amount, string? name, string? price, int categoryId, List<Tag>? tags)
        {
       
            if (name==null || price==null || categoryId<=0 || amount<=0 || tags.IsNullOrEmpty())
            {
                return false;
            }
            else if (!decimal.TryParse(price, out decimal pric) )
            {
                return false;
            }
            else if (_productRepo.GetProductByName(name) != null)
            {
                return false;
            }
            else return _productRepo.UpdateProduct(id, name, Convert.ToInt32(price), categoryId, tags); 
        }
    }
}
