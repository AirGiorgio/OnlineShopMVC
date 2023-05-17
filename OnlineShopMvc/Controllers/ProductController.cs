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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
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
        [HttpDelete]
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
