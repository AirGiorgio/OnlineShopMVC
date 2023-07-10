using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.Interfaces;

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

        [HttpGet]
        public IActionResult AddCategory()
        {
            var category = _categoryService.PrepareModel();
            return View("NewCategory", category);
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return View("CategoryDetails", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(CategoryDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCategory", newCategory);
            }

            _logger.LogInformation("W AddCategory");
            var category = _categoryService.AddCategory(newCategory.Name);
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
        public IActionResult UpdateCategory(CategoryDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryDetails", newCategory);
            }
            _logger.LogInformation("W UpdateCategory");
            var status = _categoryService.UpdateCategory(newCategory.Id, newCategory.Name);
            TempData["Message"] = status;

            return RedirectToAction("ViewCategories", "AdminCategory");
        }

        [HttpPost]
        public IActionResult RemoveCategory(int id)
        {
            _logger.LogInformation("W RemoveCategory");
            var status = _categoryService.RemoveCategory(id);
            if (status == false)
            {
                TempData["Message"] = "Kategoria już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto kategorię. Produkty należące do tej kategorii jeśli istniały przepisano" +
                    "do nowej kategorii";
            }
            return RedirectToAction("ViewCategories", "AdminCategory");
        }
    }
}