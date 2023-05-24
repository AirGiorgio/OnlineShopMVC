using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
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
    public class CategoryServices : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;

        public CategoryServices(ICategoryRepo categoryRepo, IMapper mapper, IProductRepo productRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public string AddCategory(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return null;
            }
            else if (_categoryRepo.IsCategoryNameTaken(name) == true)
            {
                return null;
            }
            else return _categoryRepo.AddCategory(name); 
        }

        public CategoriesProductsDTO GetCategoryProducts(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            var categoryDTO = _mapper.Map<CategoriesProductsDTO>(category);
            var products = _productRepo.GetProductsByCategory(id)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToList();
           categoryDTO.Products = products;
           return categoryDTO;
        }
 
        public CategoriesForListDTO GetAllCategories(string? name)
        {
            var categories = _categoryRepo.GetAllCategories(name).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            //var categoriesToShow = categories.Skip(pagesize*(pageno-1)).Take(pagesize).ToList();
            var categoriesDTO = new CategoriesForListDTO()
            {
                //PageNum= pageno,
                //PageSize=pagesize,
                Categories = categories,
                Count = categories.Count
            };
            
            return categoriesDTO;         
        }

        public CategoryDTO GetCategoryById(int id)
        {
            if (id<=0 || id ==null)
            {
                return null;
            }
            else
            {
                var category = _categoryRepo.GetCategoryById(id);
                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return categoryDTO;
            }
        }

        public bool RemoveCategory(int id)
        {
            if (id<=0 || id == null)
            {
                return false;
            }
            else return _categoryRepo.RemoveCategory(id);
        }

        public bool UpdateCategory(int id, string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return false;
            }
            else if (_categoryRepo.IsCategoryNameTaken(name)== true)
            {
                return false;
            }
            else return _categoryRepo.UpdateCategory(id, name);
        }
    }
}
