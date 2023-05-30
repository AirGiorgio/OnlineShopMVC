using Microsoft.EntityFrameworkCore.Storage;
using OnlineShopMvc.Domain.Model;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf
{
    public class Seeder
    {
        Context _context = new Context();
        public void BreedTheSeedAndNeedForSpeed()
        {
            if (_context.Database.CanConnect())
            {
                if (_context.Clients.Any())
                {
                    return;
                }
                _context.SaveChanges();
                var categories = GetCategories();
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
        }
         
        private IEnumerable<Category> GetCategories()
        {
            var clients = Enumerable.Range(1, 50).Select(_ => new Client
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
                },
            }).ToList();

            var categories = Enumerable.Range(1, 50).Select(x => new Category
            {
                Name = GenerateRandomString(8),
                Products = Enumerable.Range(5,10).Select(x => new Product
                {
                    Name = GenerateRandomString(8),
                    Price = GenerateRandomDecimal(),
                    Quantity = GenerateRandomInt(1, 100),
                    ProductTags = Enumerable.Range(1, 3).Select(x => new ProductTag
                    {
                        Tag = new Tag
                        {
                            Name = GenerateRandomString(5)
                        }
                    }).ToList(),
                    OrderProducts = Enumerable.Range(1, 3).Select(x => new OrderProduct
                    {
                        Order = new Order
                        {
                            OrderDate = GenerateRandomDateTime(),
                            TotalCost = GenerateRandomDecimal(),
                            Client = clients[GenerateRandomInt(1, clients.Count() - 1)],
                        }
                    }).ToList()
                }).ToList()
        }).ToList();
        return categories;
        }
    
        private DateTime GenerateRandomDateTime()
        {
            var startDate = new DateTime(2020, 1, 1);
            var endDate = DateTime.Now;
            var random = new Random();
            var range = endDate - startDate;
            var randomTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            return startDate + randomTimeSpan;
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
            decimal value = (decimal)(random.NextDouble() * 100);
            return Math.Round(value, 2);
        }

        private int GenerateRandomInt(int minValue, int maxValue)
        {
            var random = new Random();
            return random.Next(minValue, maxValue + 1);
        }

    }
}
