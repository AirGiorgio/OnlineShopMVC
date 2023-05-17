using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.Interfaces;

namespace OnlineShopMvc.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ILogger<AdminTagController> _logger;
        public AdminTagController(ITagService tagService, ILogger<AdminTagController> logger)
        {
            _tagService = tagService;
            _logger = logger;

        }
    
        [HttpGet]
        public IActionResult GetTagByName(string? name)
        {
            var tag = _tagService.GetTagByName(name);
            if (tag != null)
            {
                return View(tag);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult ViewTags()
        {
            var tags = _tagService.GetAllTags();
            if (tags != null)
            {
                return View(tags);
            }
            else return NotFound();
        }
        [HttpPost]
        public IActionResult AddTag(string? name)
        {
            var tag = _tagService.AddTag(name);
            if (tag.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return Ok("Dodano nowy tag");
        }
        [HttpPatch]
        public IActionResult UpdateTag(string? name)
        {
            var tag = _tagService.UpdateTag(name);
            if (tag == false)
            {
                return BadRequest();
            }
            else return Ok("Zaktualizowano tag");
        }
        [HttpDelete]
        public IActionResult RemoveTag(int id)
        {
            var tag = _tagService.RemoveTag(id);
            if (tag == false)
            {
                return BadRequest();
            }
            else return Ok("Usunięto tag");
        }
    }
}
