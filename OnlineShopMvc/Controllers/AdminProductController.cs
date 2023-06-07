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
    public class AdminProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<AdminProductController> _logger;
        public AdminProductController(IProductService productService, ILogger<AdminProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ViewProducts(int? pageSize, int? pageNo, int? categoryId, List<int> searchTags, decimal? min, decimal? max, string? name)
        {
            var products = _productService.GetAllProducts(pageSize, pageNo, categoryId, searchTags, min, max, name);
             return View(products);
            
        }
        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(int id, int? amount, string? name, string? price, int categoryId, List<Tag> productTags)
        {
            var product = _productService.UpdateProduct(id, amount, name, price, categoryId, productTags);
       
            TempData["Message"] = product;
            return RedirectToAction("ViewProducts");
        }

        [HttpPost]
        public IActionResult AddProduct(int? amount, string? name, string? price, int categoryId, List<Tag> productTags)
        {
            var product = _productService.AddProduct(amount,name,price,categoryId,productTags);
            
            TempData["Message"] = product;
            return RedirectToAction("ViewProducts");
        }
        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var status = _productService.RemoveProduct(id);
            if (status == false)
            {
                TempData["Message"] = "Produkt już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto produkt";
            }
            return RedirectToAction("ViewProducts");
        }
    }
}
