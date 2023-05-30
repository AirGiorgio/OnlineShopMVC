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
            return context.Products.Include(x=>x.ProductTags).Include(x=>x.Category);
        }

        public Product GetProductById(int id)
        {
            return context.Products.Include(x => x.ProductTags).Include(x => x.Category).SingleOrDefault(i => i.Id == id);
        }
        public IQueryable GetProductsFromTags(List<Tag> tags) //nie zadziała
        {
            return context.Products.Where(x => x.ProductTags== tags).OrderBy(x => x.Price);
        }
        public IQueryable GetProductByName(string name)
        {
            return context.Products.Include(x => x.ProductTags).Include(x => x.Category).Where(i => i.Name.StartsWith(name)).OrderBy(x => x.Price);
        }
        public IQueryable GetProductsByCategory(int id)
        {
            return context.Products.Include(x => x.ProductTags).Include(x => x.Category).Where(x => x.CategoryId == id).OrderBy(x => x.Price);
        }
        public bool UpdateProductAmount(int id, int quantity)
        {
            var productFound = GetProductById(id);
            if (productFound != null)
            {
                productFound.Quantity = quantity;
                context.Update(productFound);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool UpdateProduct(int id, string name, decimal price, int categoryId, List<Tag> tags)
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
                    //productFound.Tags = tags;
                    context.Update(productFound);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
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

        public string AddProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
            return product.Name;           
        }

        public IQueryable GetProductsByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable GetProductsFromValue(decimal min, decimal max)
        {
            return context.Products.Include(x => x.ProductTags).Include(x => x.Category).Where(x => x.Price > min && x.Price < max).OrderBy(x => x.Price);
        }
    }
}
