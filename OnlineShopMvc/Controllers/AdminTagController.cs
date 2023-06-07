using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ILogger<AdminProductController> _logger;
        public AdminTagController(ITagService tagService, ILogger<AdminProductController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ViewTags(int? pageSize, int? pageNo, string? name)
        {
            var tags = _tagService.GetAllTags(pageSize, pageNo, name);
             return View(tags);
            
        }
        [HttpPost]
        public IActionResult AddTag(string? name)
        {
            var tag = _tagService.AddTag(name);
            if (tag.IsNullOrEmpty())
            {
                TempData["Message"] = "Nazwa jest pusta";
            }
            else
            {
                TempData["Message"] = tag;
            }
            return RedirectToAction("ViewTags", "AdminTag");
        }
        [HttpGet]
        public IActionResult UpdateTag(int id, string? name)
        {
            var tag = _tagService.UpdateTag(id, name);
            TempData["Message"] = tag;
            return RedirectToAction("ViewTags", "AdminTag");
        }

        [HttpGet]
        public IActionResult TagProducts(int id)
        {
            var category = _tagService.GetTagProducts(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult RemoveTag(int id)
        {
            var tag = _tagService.RemoveTag(id);
            if (tag == false)
            {
                TempData["Message"] = "Tag już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto tag";
            }
            return RedirectToAction("ViewTags", "AdminTag");
        }
    }
}
