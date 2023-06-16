﻿using AutoMapper;
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

        public ProductsForListDTO GetAllProducts(int? pageSize, int? pageNo, CategoryDTO searchCategory, List<TagDTO> searchTags, decimal? min, decimal? max, string? name)
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
            if (searchCategory.Name.IsNullOrEmpty() && !min.HasValue && !max.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetAllProducts()
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();

            }
            else if (!searchCategory.Name.IsNullOrEmpty() && !min.HasValue && !max.HasValue && name.IsNullOrEmpty() && searchTags.IsNullOrEmpty())
            {
                products = _productRepo.GetProductsByCategory(searchCategory.Id)
               .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                productsForListDTO.SearchCategory = searchCategory;  
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
                List<Tag> sTags = new List<Tag>();
                foreach (var item in searchTags)
                {
                    sTags.Add(_mapper.Map<Tag>(item));
                }
                products = _productRepo.GetProductsFromTags(sTags)
                   .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
                productsForListDTO.SearchTags = searchTags;
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
            else return _productRepo.UpdateProduct(_mapper.Map<Product>(product));
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
            else return _productRepo.AddProduct(_mapper.Map<Product>(product));
        }
    }
}
