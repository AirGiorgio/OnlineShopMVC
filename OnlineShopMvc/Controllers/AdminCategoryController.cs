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
        public IActionResult ViewCategories(int pageSize, int? pageNo, string? name)  
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            var category = _categoryService.GetAllCategories(pageSize, pageNo.Value, name);
            if (category != null)
            {
                return View(category);
            }
            else return NotFound();
        }

        [HttpGet]
        public IActionResult CategoryProducts(int id)
        {
           
            var category = _categoryService.GetCategoryProducts(id);
            if (category != null)
            {
                return View(category);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult AddCategory(string? name) 
        {
            var category = _categoryService.AddCategory(name);
            if (category.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewCategories", "AdminCategory");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id, string? name)  
        {
            var category = _categoryService.UpdateCategory(id, name);
            if (category == false)
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewCategories", "AdminCategory");
        }
        [HttpPost]
        public IActionResult RemoveCategory(int id)  
        {
            var category = _categoryService.RemoveCategory(id);
            if (category == false)
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewCategories", "AdminCategory");
        }

    }
}
