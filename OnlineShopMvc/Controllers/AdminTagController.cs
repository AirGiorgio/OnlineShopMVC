using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Interfaces;

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
            _logger.LogInformation("W ViewTags");
            var tags = _tagService.GetAllTags(pageSize, pageNo, name);
            return View(tags);
        }

        [HttpGet]
        public IActionResult AddTag()
        {
            var tag = _tagService.PrepareModel();
            return View("NewTag", tag);
        }

        [HttpGet]
        public IActionResult UpdateTag(int id)
        {
            var tag = _tagService.GetTagById(id);
            return View("TagDetails", tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTag(TagDTO newTag)
        {
            if (!ModelState.IsValid)
            {
                return View("NewTag", newTag);
            }
            _logger.LogInformation("W AddTag");
            var tag = _tagService.AddTag(newTag.Name);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTag(TagDTO newTag)
        {
            if (!ModelState.IsValid)
            {
                return View("TagDetails", newTag);
            }
            _logger.LogInformation("W UpdateTag");
            var status = _tagService.UpdateTag(newTag.Id, newTag.Name);
            TempData["Message"] = status;
            return RedirectToAction("ViewTags", "AdminTag");
        }

        [HttpGet]
        public IActionResult TagProducts(int id)
        {
            _logger.LogInformation("W TagProducts");
            var category = _tagService.GetTagProducts(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult RemoveTag(int id)
        {
            _logger.LogInformation("W RemoveTag");
            var status = _tagService.RemoveTag(id);
            if (status == false)
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