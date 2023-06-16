using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Repo;

namespace OnlineShopMvc.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<AdminCategoryController> _logger;
        public AdminCategoryController(ICategoryService categoryService, ILogger<AdminCategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
    
        [HttpGet]
        public IActionResult ViewCategories(int? pageSize, int? pageNo, string? name)  
        {
            _logger.LogInformation("W ViewCategories");
            var categories = _categoryService.GetAllCategories(pageSize, pageNo, name);
            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoryProducts(int id)
        {
            _logger.LogInformation("W CategoryProducts");
            var category = _categoryService.GetCategoryProducts(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(CategoriesForListDTO categories) 
        {
            if (!ModelState.IsValid)
            {
                return View("ViewCategories", categories);
            }

            _logger.LogInformation("W AddCategory");
             var category = _categoryService.AddCategory(categories.NewCategory.Name);
            if (category.IsNullOrEmpty())
            {
                TempData["Message"] = "Nazwa jest pusta";
            }
            else
            {
                TempData["Message"] = category;
            }
            return RedirectToAction("ViewCategories", "AdminCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(CategoriesForListDTO categories)
        {
            if (!ModelState.IsValid)
            {
                return View("ViewCategories", categories);
            }
            _logger.LogInformation("W UpdateCategory");
            var category = _categoryService.UpdateCategory(categories.NewCategory.Id, categories.NewCategory.Name);
            TempData["Message"] = category;
            
            return RedirectToAction("ViewCategories", "AdminCategory");
        }
        [HttpPost]
        public IActionResult RemoveCategory(int id)  
        {
            _logger.LogInformation("W RemoveCategory");
            var category = _categoryService.RemoveCategory(id);
            if (category == false)
            {
                TempData["Message"] = "Kategoria już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto kategorię i jej produkty";
            }
             return RedirectToAction("ViewCategories", "AdminCategory");
        }
    }
}
