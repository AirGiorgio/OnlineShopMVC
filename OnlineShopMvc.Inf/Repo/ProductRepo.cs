using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
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
            return context.Products.Include(x=>x.Tags).Include(x=>x.Category);
        }

        public Product GetProductById(int id)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).SingleOrDefault(i => i.Id == id);
        }
        public IQueryable GetProductsFromTags(List<int> tags) 
        {
            return context.Products.Where(p => p.Tags.Any(t => tags.Contains(t.Id))).OrderBy(p => p.Price);
        }
        public IQueryable GetProductByName(string name)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).Where(i => i.Name.StartsWith(name)).OrderBy(x => x.Price);
        }
        public IQueryable GetProductsByCategory(int id)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).Where(x => x.CategoryId == id).OrderBy(x => x.Price);
        }
   
        public string UpdateProduct(int id, int amount, string name, decimal price, int categoryId, List<Tag> tags)
        {
            var productFound = GetProductById(id);
            if (productFound != null)
            {
                productFound.Name = name;
                productFound.Price = price;
                var cat = context.Categories.Where(x => x.Id == categoryId).SingleOrDefault();
                if (cat!=null || !tags.IsNullOrEmpty())
                {
                    productFound.Category = cat;
                    productFound.Tags = tags;
                    context.Update(productFound);
                    context.SaveChanges();
                    return "Uaktualniono produkt";
                }
                else return "Błędne kategorie lub tagi";
            }
            else return "Nie znaleziono produktu";
        }
        public bool RemoveProduct(int id)  
        {
            var product = GetProductById(id);
            if (product != null)
            {
                context.Remove(product);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public string AddProduct(int amount, string name, decimal price, int categoryId, List<Tag> tags)
        {
             
            Product product = new Product()
            {
                Quantity = amount,
                Price = price,
                Name = name,
                CategoryId = categoryId,
                Tags = tags
            };
            context.Add(product);
            context.SaveChanges();
            return product.Name;           
        }

        public IQueryable GetProductsFromValue(decimal min, decimal max)
        {
            return context.Products.Include(x => x.Tags).Include(x => x.Category).Where(x => x.Price > min && x.Price < max).OrderBy(x => x.Price);
        }
    }
}
