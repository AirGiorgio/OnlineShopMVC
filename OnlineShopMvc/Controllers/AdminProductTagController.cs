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
        public IActionResult ViewTags(string? name)
        {
            //if (!pageNo.HasValue)
            //{
            //    pageNo = 1;
            //}
            var tags = _tagService.GetAllTags(name);
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
        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }
            else return BadRequest();
        }
        [HttpGet]  //klienckie
        public IActionResult GetProductsFromTags(List<Tag> tags)
        {
            var products = _productService.GetProductsFromTags(tags);
            if (products != null)
            {
                return View(products);
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpGet]
        public IActionResult GetProductsFromValue(decimal? min, decimal? max)
        {
            var products = _productService.GetProductsFromValue(min, max);
            if (products != null)
            {
                return View("ViewProducts", products);
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpGet] 
        public IActionResult GetProductByName(string? name)
        {
            var products = _productService.GetProductByName(name);
            if (products != null)
            {
                return View("ViewProducts",products);
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpGet]
        public IActionResult GetProductsByCategory(int id)
        {
            var products = _productService.GetProductsByCategory(id);
            if (products != null)
            {
                return View(products);
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpPost]
        public IActionResult AddProduct(string? name, string? price, string? quantity, int categoryId, List<Tag> productTags)
        {
            var product = _productService.AddProduct(name,price,quantity,categoryId, productTags);
            if (product.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return RedirectToAction("ViewProducts");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id, string? name, string? price, int categoryId, List<Tag> productTags)
        {
            var Product = _productService.UpdateProduct(id,name,price,categoryId,productTags);
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
