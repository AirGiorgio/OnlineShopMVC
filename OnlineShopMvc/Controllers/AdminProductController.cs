using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
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
        public IActionResult ViewProducts(int? pageSize, int? pageNo, int? searchCategory, List<int> searchTags, decimal? min, decimal? max, string? name)
        {
            _logger.LogInformation("W ViewProducts");
            var products = _productService.GetAllProducts(pageSize, pageNo, searchCategory, searchTags, min, max, name);
             return View(products);
            
        }
        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            _logger.LogInformation("W ProductDetails");
            var product = _productService.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct( ProductDetailsDTO product)
        {
            ModelState.Remove("Tag");
            ModelState.Remove("Category");
            ModelState.Remove("Tags");
            ModelState.Remove("Categories");
            if (!ModelState.IsValid)
            {
                var vproduct = _productService.PrepareModel();
                product.Tags = vproduct.Tags;
                product.Categories = vproduct.Categories;
                TempData["Message"] = "Kategorie lub tagi są niepoprawnie wybrane";
                return View("ProductDetails", product);
            }
            _logger.LogInformation("W UpdateProduct");
            var status = _productService.UpdateProduct(product);
       
            TempData["Message"] = status;
            return RedirectToAction("ViewProducts");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            _logger.LogInformation("W AddProduct typu Get");
            var product = _productService.PrepareModel();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductDetailsDTO product)
        {
            ModelState.Remove("Tag");
            ModelState.Remove("Category");
            ModelState.Remove("Tags");
            ModelState.Remove("Categories");
            if (!ModelState.IsValid)
            {
                var vproduct = _productService.PrepareModel();
                product.Tags = vproduct.Tags;
                product.Categories = vproduct.Categories;
                TempData["Message"] = "Kategorie lub tagi są niepoprawnie wybrane";
                return View("AddProduct", product);
            }
            _logger.LogInformation("W AddProduct typu Post");
            var status = _productService.AddProduct(product);
            
            TempData["Message"] = status;
            return RedirectToAction("ViewProducts");
        }
        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            _logger.LogInformation("W RemoveProduct");
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
