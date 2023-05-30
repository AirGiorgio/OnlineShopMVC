using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            var category = _categoryService.GetAllCategories(pageSize, pageNo, name);
            return View(category);
           
        }

        [HttpGet]
        public IActionResult CategoryProducts(int id)
        {
            var category = _categoryService.GetCategoryProducts(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult AddCategory(string? name) 
        {
            var category = _categoryService.AddCategory(name);
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

        [HttpGet]
        public IActionResult UpdateCategory(int id, string? name)  
        {
            var category = _categoryService.UpdateCategory(id, name);
            TempData["Message"] = category;
            return RedirectToAction("ViewCategories", "AdminCategory");
        }
        [HttpPost]
        public IActionResult RemoveCategory(int id)  
        {
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
