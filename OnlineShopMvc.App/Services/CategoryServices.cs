using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
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

        public CategoryServices(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public string AddCategory(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa kategorii");
            }
            else if (GetCategoryByName(name) != null)
            {
                throw new ArgumentException("Kategoria o tej nazwie już istnieje");
            }
            else return _categoryRepo.AddCategory(name); 
        }

        public CategoriesForListDTO GetAllCategories(int pagesize, int pageno)
        {
            var categories = _categoryRepo.GetAllCategories(pagesize,pageno).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            var categoriesToShow = categories.Skip(pagesize*(pageno-1)).Take(pagesize).ToList();
            var categoriesDTO = new CategoriesForListDTO()
            {
                PageNum= pageno,
                PageSize=pagesize,
                Categories = categories,
                Count = categories.Count
            };
            
            return categoriesDTO;         
        }

        public CategoryDTO GetCategoryById(int id)
        {
            if (id<=0 || id ==null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator kategorii");
            }
            else
            {
                var category = _categoryRepo.GetCategoryById(id);
                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return categoryDTO;
            }
        }

        public CategoryDTO GetCategoryByName(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa kategorii");
            }
            else
            {
                var category = _categoryRepo.GetCategoryByName(name);
                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return categoryDTO;
            }
        }

        public bool RemoveCategory(int id)
        {
            if (id<=0 || id == null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator kategorii");
            }
            else return _categoryRepo.RemoveCategory(id);
        }

        public bool UpdateCategory(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa kategorii");
            }
            else if (GetCategoryByName(name) != null)
            {
                throw new ArgumentException("Kategoria o tej nazwie już istnieje");
            }
            else return _categoryRepo.UpdateCategory(name);
        }
    }
}
