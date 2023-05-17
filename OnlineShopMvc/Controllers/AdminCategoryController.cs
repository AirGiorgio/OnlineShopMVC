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
        public IActionResult GetCategoryByName(string? name)  
        {
            var category = _categoryService.GetCategoryByName(name);
            if (category != null)
            {
                return View(category);
            }
            else return NotFound();
        }
     
        [HttpGet]
        public IActionResult ViewCategories(int pageSize, int pageNo)  
        {
            var category = _categoryService.GetAllCategories(pageSize, pageNo);
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
            else return Ok("Dodano nową kategorię");
        }

        [HttpPatch]
        public IActionResult UpdateCategory(string? name)  
        {
            var category = _categoryService.UpdateCategory(name);
            if (category == false)
            {
                return BadRequest();
            }
            else return Ok("Zaktualizowano kategorię");
        }
        [HttpDelete]
        public IActionResult RemoveCategory(int id)  
        {
            var category = _categoryService.RemoveCategory(id);
            if (category == false)
            {
                return BadRequest();
            }
            else return Ok("Usunięto kategorię");
        }

    }
}
