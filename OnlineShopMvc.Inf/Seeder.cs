using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf
{
    public class Seeder
    {
      
        public void SeedData()
        {
          
        // Create clients with addresses
        var clients = Enumerable.Range(1, 2).Select(_ => new Client
            {
                Name = GenerateRandomString(5),
                Surname = GenerateRandomString(5),
                EmailAdress = GenerateRandomString(10) + "@example.com",
                Telephone = GenerateRandomString(10),
                Address = new Address
                {
                    Street = GenerateRandomString(10),
                    BuildingNumber = GenerateRandomString(1),
                    FlatNumber = GenerateRandomString(1),
                    City = GenerateRandomString(10),
                    ZipCode = GenerateRandomString(5)
                }
            }).ToList();

            // Create categories with products and tags
            var categories = Enumerable.Range(1, 2).Select(_ => new Category
            {
                Name = GenerateRandomString(8),
                Products = Enumerable.Range(1, 2).Select(__ => new Product
                {
                    Name = GenerateRandomString(8),
                    Price = GenerateRandomDecimal(),
                    Quantity = GenerateRandomInt(1, 10),
                    Tags = Enumerable.Range(1, 2).Select(___ => new Tag
                    {
                        Name = GenerateRandomString(5)
                    }).ToList()
                }).ToList()
            }).ToList();

            // Create orders with products
            var orders = Enumerable.Range(1, 2).Select(_ => new Order
            {
                Client = clients[GenerateRandomInt(0, clients.Count - 1)],
                OrderDate = DateTime.Now,
                Products = Enumerable.Range(1, 2).Select(__ =>
                {
                    var category = categories[GenerateRandomInt(0, categories.Count - 1)];
                    return category.Products.ElementAt(GenerateRandomInt(0, category.Products.Count - 1));
                }).ToList()
            }).ToList();

            // Add entities to the context and save changes
            //using (context)
            //{
            //    context.Clients.AddRange(clients);
            //    context.Categories.AddRange(categories);
            //    context.Orders.AddRange(orders);
            //    context.SaveChanges();
            //}
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private decimal GenerateRandomDecimal()
        {
            var random = new Random();
            return Convert.ToDecimal(random.NextDouble() * 100);
        }

        private int GenerateRandomInt(int minValue, int maxValue)
        {
            var random = new Random();
            return random.Next(minValue, maxValue + 1);
        }
    }
}
