using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
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
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductServices(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public ProductDetailsDTO GetProductById(int id)
        {
            if (id <= 0 || id==null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator produktu");
            }
            else
            {
                var product = _productRepo.GetProductById(id);
                var productDTO = _mapper.Map<ProductDetailsDTO>(product);     
                return productDTO;
            }
            
        }

        public ProductsForListDTO GetProductsByCategory(Category? category)
        {
            if (category == null)
            {
                throw new ArgumentException("Błędna kategoria");
            }
            else if (category.Name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa produktu");
            }
            else if (category.Products.Count ==0)
            {
                throw new ArgumentException("Nie przypisano żadnego produktu do kategorii");
            }
            else
            {
                var products = _productRepo.GetProductsByCategory(category)
                    .ProjectTo<ProductDTO> (_mapper.ConfigurationProvider).ToList();
                var productsDTO = new ProductsForListDTO()
                {
                  Products = products
                };

                return productsDTO;
            }
        }

        public string AddProduct(string? name, string? price, string? quantity, Category category, List<Tag> productTags)
        {
            if (category ==null || productTags.Count==0)
            {
                throw new ArgumentException("Nieprawidłowa kategoria lub tagi produktu");
            }

            if (name == null || price == null || quantity == null || !decimal.TryParse(price, out decimal pric) || !int.TryParse(quantity, out int quant))
            {
                throw new ArgumentException("Nieprawidłowe dane produktu");
            }
           
            Product product = new Product();
            product.Name = name;
            product.Price = Convert.ToDecimal(price);
            product.Quantity = Convert.ToInt32(quantity);
            product.CategoryId = category.Id;
            product.Category = category;
            product.Tags = productTags;

            return _productRepo.AddProduct(product);
        }

        public bool RemoveProduct(int id)
        {
            if (id <= 0 || id == null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator produktu");
            }

            return _productRepo.RemoveProduct(id);
        }

        public ProductsForListDTO GetAllProducts()
        {  
             var products = _productRepo.GetAllProducts()
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
            var productsDTO = new ProductsForListDTO()
            {
                Products = products
            };

            return productsDTO;
        }

        public ProductsForListDTO GetProductsFromTags(List<Tag> tags)
        {
            if (tags.Count ==0 && tags.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowe wartości tagów produktu");
            }
            else
            {
                var products = _productRepo.GetProductsFromTags(tags).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                var productsDTO = new ProductsForListDTO()
                {
                    Products = products
                };

                return productsDTO;
            }
        }

        public ProductDTO GetProductByName(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa produktu");
            }
            else
            {
                var product = _productRepo.GetProductByName(name);
                var productDTO = _mapper.Map<ProductDTO>(product);
                return productDTO;
            }
        }

        public bool UpdateProduct(Product? product, string? name, string? price, string? quantity, Category category, List<Tag> productTags)
        {
            if (product == null || category == null || productTags.Count ==0) 
            {
                throw new ArgumentException("Nieprawidłowe dane produktu");
            }
            else if (name==null || price==null || quantity == null)
            {
                throw new ArgumentException("Nieprawidłowe dane produktu");
            }
            else if (!decimal.TryParse(price, out decimal pric) || !int.TryParse(quantity, out int quant))
            {
                throw new ArgumentException("Nieprawidłowe dane produktu");
            }
            else if (GetProductByName(name) != null)
            {
                throw new ArgumentException("Nazwa produktu już istnieje");
            }
            return _productRepo.UpdateProduct(product); 
        }

        public bool UpdateProductAmount(Product? product, string? quantity)
        {
            if(product == null)
            {
                throw new ArgumentException("Produkt nie istnieje");
            }
            else if (!int.TryParse(quantity, out int quant) || quantity == null)
            {
                throw new ArgumentException("Nieprawidłowa ilość produktu");
            }
            else return _productRepo.UpdateProductAmount(product, Convert.ToInt32(quantity));
        }
    }
}
