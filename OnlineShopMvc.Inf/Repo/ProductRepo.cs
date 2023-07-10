using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.Domain.Model;
using OnlineShopMvc.Inf.Data;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly Context context;
        public ProductRepo(Context context)
        {
            this.context = context;
        }
        public IQueryable GetAllProducts() 
        {
            return context.Products.Where(x=>x.IsActive==true).Include(x=>x.Tags).Include(x=>x.Category);
        }

        public Product GetProductById(int id)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).SingleOrDefault(i => i.Id == id);
        }
        public IQueryable GetProductsFromTags(List<int> tags) 
        {
            var col = context.Products.Where(p => p.IsActive == true);
            foreach (var item in tags)
            {
                col = col.Where(p => p.Tags.Any(t => t.Id == item));
            }
            return col.OrderBy(p => p.Price); ;
        }
        public IQueryable GetProductByName(string name)
        {
            return context.Products.Include(x => x.Tags ).Include(x => x.Category).Where(i => i.Name.StartsWith(name) && i.IsActive == true).OrderBy(x => x.Price);
            
        }
        public IQueryable GetProductsByCategory(int id)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).Where(x => x.CategoryId == id && x.IsActive == true).OrderBy(x => x.Price);
        }
        public bool IsProductNameTaken(string? name)
        {
            if (context.Products.Any(x => x.Name == name))
            {
                return true;
            }
            else return false;
        }
        public bool IsProductNameTaken(string? name, int id)
        {
            if (context.Products.Any(x => x.Name == name && x.Id != id))  
            {
                return true;
            }
            else return false;
        }
        public string UpdateProduct(Product product)
        {
            var productFound = GetProductById(product.Id);
            if (productFound != null)
            {
                productFound.IsActive = true;
                productFound.Name = product.Name;
                productFound.Price = product.Price;
                productFound.Quantity = product.Quantity;
                productFound.Category = product.Category;
                productFound.Tags = product.Tags;
                
                context.Update(productFound);
                context.SaveChanges();
                return "Uaktualniono produkt";
            }
            else return "Nie znaleziono produktu";
        }
        public bool RemoveProduct(int id)  
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.IsActive = false;
                context.Update(product);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public string AddProduct(Product product)
        {
          
            context.Add(product);
            context.SaveChanges();
            return "Dodano produkt";        
        }

        public IQueryable GetProductsFromValue(decimal? min, decimal? max)
        {
            if (min.HasValue && !max.HasValue)
            {
                max = context.Products.Max(x => x.Price);
            }
            else if (max.HasValue && !min.HasValue)
            {
                min = 0;
            }
            return context.Products.Include(x => x.Tags).Include(x => x.Category).Where(x => x.Price > min && x.Price < max && x.IsActive==true).OrderBy(x => x.Price);
        }
    }
}
