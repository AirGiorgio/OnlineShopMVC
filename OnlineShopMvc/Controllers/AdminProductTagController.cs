using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMVC.Domain.Model;
using System.Collections.Generic;

namespace OnlineShopMvc.Controllers
{
    public class AdminProductTagController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITagService _tagService;
        private readonly ILogger<AdminProductTagController> _logger;
        public AdminProductTagController(IProductService productService, ILogger<AdminProductTagController> logger, ITagService tagService)
        {
            _productService = productService;
            _logger = logger;
            _tagService = tagService;
        }
        //[HttpGet]
        //public IActionResult ViewTags(string? name)
        //{
        //    var tags = _tagService.GetAllTags(name);
        //    if (tags != null)
        //    {
        //        return View(tags);
        //    }
        //    else return NotFound();
        //}
        //[HttpPost]
        //public IActionResult AddTag(string? name)
        //{
        //    var tag = _tagService.AddTag(name);
        //    if (tag.IsNullOrEmpty())
        //    {
        //        return BadRequest();
        //    }
        //    else return Ok("Dodano nowy tag");
        //}
        //[HttpGet]
        //public IActionResult UpdateTag(int id, string? name)
        //{
        //    var tag = _tagService.UpdateTag(id, name);
        //    if (tag == false)
        //    {
        //        return BadRequest();
        //    }
        //    else return Ok("Zaktualizowano tag");
        //}
        //[HttpPost]
        //public IActionResult RemoveTag(int id)
        //{
        //    var tag = _tagService.RemoveTag(id);
        //    if (tag == false)
        //    {
        //        return BadRequest();
        //    }
        //    else return Ok("Usunięto tag");
        //}

        [HttpGet]
        public IActionResult ViewProducts(int? pageSize, int? pageNum, int? categoryId, List<Tag> tags, decimal? min, decimal? max, string? name)
        {
            var products = _productService.GetAllProducts(pageSize, pageNum, categoryId, tags, min, max, name);
            if (products != null)
            {
                return View(products);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }
            else return BadRequest();
        }

        [HttpPost]
        public IActionResult AddOrUpdateProduct(int id, int? amount, string? name, string? price, int categoryId, List<Tag> productTags)
        {
            var Product = _productService.AddOrUpdateProduct(id,amount,name,price,categoryId,productTags);
            if (Product == false)
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var product = _productService.RemoveProduct(id);
            if (product == false)
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewProducts");
        }
    }
}
