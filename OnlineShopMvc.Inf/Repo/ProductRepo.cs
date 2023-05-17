using Azure;
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
            return context.Products;
        }

        public Product GetProductById(int? id)
        {
            return context.Products.SingleOrDefault(i => i.Id == id);
        }
        public IQueryable GetProductsFromTags(List<Tag> tags)
        {
            return context.Products.Where(x => x.Tags == tags);
        }
        public Product GetProductByName(string name)
        {
            return context.Products.Where(i => i.Name.StartsWith(name)).SingleOrDefault();
        }
        public IQueryable GetProductsByCategory(Category category)
        {
            return context.Products.Where(x => x.Category == category);
        }
        public bool UpdateProductAmount(Product? product, int quantity)
        {
            var productFound = GetProductById(product.Id);
            if (productFound != null)
            {
                product.Quantity = quantity;
                context.Update(productFound);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool UpdateProduct(Product product)
        {
            var productFound = GetProductById(product.Id);
            if (productFound != null)
            {
                productFound.Name = product.Name;
                productFound.Price = product.Price;
                productFound.Quantity = product.Quantity;
                productFound.Category = product.Category;
                context.Update(productFound);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool RemoveProduct(int? id)  
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
    }
}
