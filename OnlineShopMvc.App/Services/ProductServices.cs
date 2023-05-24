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

        public ProductsForListDTO GetProductsByCategory(int? id)
        {
            if (id<=0 || id== null)
            {
                return null;
            }
            else 
            {
                var products = _productRepo.GetProductsByCategory(id.Value)
                    .ProjectTo<ProductDTO> (_mapper.ConfigurationProvider).ToList();
                var productsDTO = new ProductsForListDTO()
                {
                  Products = products,
                  Count = products.Count
                };

                return productsDTO;
            }
        }

        public string AddProduct(string? name, string? price, string? quantity, int categoryId, List<Tag>? tags)
        {
            if (name == null || price == null || quantity == null || !decimal.TryParse(price, out decimal pric) || !int.TryParse(quantity, out int quant) || tags.IsNullOrEmpty())
            {
                return null;
            }
           
            Product product = new Product();
            product.Name = name;
            product.Price = Convert.ToDecimal(price);
            product.Quantity = Convert.ToInt32(quantity);
            product.CategoryId = categoryId;
            product.Tags = tags;

            return _productRepo.AddProduct(product);
        }

        public bool RemoveProduct(int id)
        {
            if (id <= 0 || id == null)
            {
                return false;
            }

            return _productRepo.RemoveProduct(id);
        }

        public ProductsForListDTO GetAllProducts()
        {  
             var products = _productRepo.GetAllProducts()
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
          
            var categories = _categoryRepo.GetAllCategories(null)
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();  
            var tags = _tagRepo.GetAllTags(null)
                .ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();

            var productsDTO = new ProductsForListDTO()
            {
                Products = products,
                Categories = categories,
                Tags = tags,
                Count = products.Count
            };

            return productsDTO;
        }

        public ProductsForListDTO GetProductsFromTags(List<Tag> tags)
        {
            if (tags.Count ==0 && tags.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                var products = _productRepo.GetProductsFromTags(tags).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                var productsDTO = new ProductsForListDTO()
                {
                    Products = products,
                    Count = products.Count
                };

                return productsDTO;
            }
        }

        public ProductsForListDTO GetProductByName(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return GetAllProducts();
            }
            else
            {
                var products = _productRepo.GetProductByName(name).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                var productsDTO = new ProductsForListDTO()
                {
                    Products = products,
                    Count = products.Count
                };
                return productsDTO;
            }
        }

        public bool UpdateProduct(int id, string? name, string? price, int categoryId, List<Tag>? tags)
        {
       
            if (name==null || price==null || categoryId<=0 || tags.IsNullOrEmpty())
            {
                return false;
            }
            else if (!decimal.TryParse(price, out decimal pric) )
            {
                return false;
            }
            else if (GetProductByName(name) != null)
            {
                return false;
            }
            else return _productRepo.UpdateProduct(id, name, Convert.ToInt32(price), categoryId, tags); 
        }

        public bool UpdateProductAmount(int id, string? quantity)
        {
            if(id<=0 || id == null)
            {
                return false;
            }
            else if (!int.TryParse(quantity, out int quant) || quantity == null)
            {
                return false;
            }
            else return _productRepo.UpdateProductAmount(id, Convert.ToInt32(quantity));
        }

        public ProductsForListDTO GetProductsByCategory(int id)
        {
            throw new NotImplementedException();
        }

        public ProductsForListDTO GetProductsFromValue(decimal? min, decimal? max)
        {
            if (!min.HasValue || !max.HasValue || min <= 0 || max <= 0)
            {
                min = 0;
                max = 0;
            }
            var products = _productRepo.GetProductsFromValue(min.Value, max.Value).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            var productsDTO = new ProductsForListDTO()
            {
                Products = products,
                Count = products.Count
            };
            return productsDTO;
        }
    }
}
