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
        [HttpGet]
        public IActionResult ViewTags(int pageSize, int? pageNo, string? name)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            var tags = _tagService.GetAllTags(pageSize, pageNo.Value, name);
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
        [HttpGet]
        public IActionResult UpdateTag(int id, string? name)
        {
            var tag = _tagService.UpdateTag(id, name);
            if (tag == false)
            {
                return BadRequest();
            }
            else return Ok("Zaktualizowano tag");
        }
        [HttpPost]
        public IActionResult RemoveTag(int id)
        {
            var tag = _tagService.RemoveTag(id);
            if (tag == false)
            {
                return BadRequest();
            }
            else return Ok("Usunięto tag");
        }

        [HttpGet]
        public IActionResult ViewProducts()
        {
            var products = _productService.GetAllProducts();
            if (products != null)
            {
                return View(products);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetProductsFromTags(List<Tag> tags)
        {
            var products = _productService.GetProductsFromTags(tags);
            if (products != null)
            {
                return View(products);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetProductByName(string? name)
        {
            var product = _productService.GetProductByName(name);
            if (product != null)
            {
                return View(product);
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult GetProductsByCategory(Category? category)
        {
            var products = _productService.GetProductsByCategory(category);
            if (products != null)
            {
                return View(products);
            }
            else return NotFound();
        }
        [HttpPost]
        public IActionResult AddProduct(string? name, string? price, string? quantity, Category category, List<Tag> productTags)
        {
            var product = _productService.AddProduct(name,price,quantity,category, productTags);
            if (product.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return Ok("Dodano nowy produkt");
        }

        [HttpPatch]
        public IActionResult UpdateProduct(Product? product, string? name, string? price, string? quantity, Category category, List<Tag> productTags)
        {
            var Product = _productService.UpdateProduct(product,name,price,quantity,category,productTags);
            if (Product == false)
            {
                return BadRequest();
            }
            else return Ok("Zaktualizowano produkt");
        }
        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var product = _productService.RemoveProduct(id);
            if (product == false)
            {
                return BadRequest();
            }
            else return Ok("Usunięto produkt");
        }
    }
}
