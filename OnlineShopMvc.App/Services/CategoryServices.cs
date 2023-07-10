using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;

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
                return null;
            }
            else if (_categoryRepo.IsCategoryNameTaken(name) == true)
            {
                return "Nazwa kategorii jest zajęta";
            }
            else return _categoryRepo.AddCategory(name);
        }

        public CategoriesProductsDTO GetCategoryProducts(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            var categoryDTO = _mapper.Map<CategoriesProductsDTO>(category);

            return categoryDTO;
        }

        public CategoriesForListDTO GetAllCategories(int? pageSize, int? pageNo, string? name)
        {
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }

            var categories = _categoryRepo.GetAllCategories(name).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            var categoriesToShow = categories.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();
            var categoriesDTO = new CategoriesForListDTO()
            {
                SearchString = name,
                PageNum = pageNo.Value,
                PageSize = pageSize.Value,
                Categories = categoriesToShow,
                Count = categories.Count
            };
            return categoriesDTO;
        }

        public CategoryDTO GetCategoryById(int id)
        {
            if (id <= 0 || id == null)
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

        public CategoryDTO PrepareModel()
        {
            CategoryDTO newCategory = new CategoryDTO();
            return newCategory;
        }

        public bool RemoveCategory(int id)
        {
            if (id <= 0 || id == null)
            {
                return false;
            }
            else return _categoryRepo.RemoveCategory(id);
        }

        public string UpdateCategory(int id, string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return "Niepoprawna nazwa";
            }
            else if (_categoryRepo.IsCategoryNameTaken(name) == true)
            {
                return "Nazwa jest zajęta";
            }
            else return _categoryRepo.UpdateCategory(id, name);
        }
    }
}