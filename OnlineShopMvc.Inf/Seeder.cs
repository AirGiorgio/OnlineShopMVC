﻿using Microsoft.EntityFrameworkCore.Storage;
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
        private readonly Context _context;
        public Seeder(Context context)
        {
            _context = context;
        }
        
        public void BreedTheSeedAndNeedForSpeed()
        {
            if (_context.Database.CanConnect())
            {
                if (_context.Clients.Any())
                {
                    return;
                }

                var categories = GetData();
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
              
            }
        }

        private IEnumerable<Category> GetData()
        {
            var clients = Enumerable.Range(1, 50).Select(_ => new Client
            {
                Name = GenerateRandomName(),
                Surname = GenerateRandomSurname(),
                EmailAdress = GenerateRandomName() + "@example.com",
                Telephone = GenerateRandomNumber(10),
                Address = new Address
                {
                    Street = GenerateRandomStreet(),
                    BuildingNumber = GenerateRandomNumber(1),
                    FlatNumber = GenerateRandomNumber(1),
                    City = GenerateRandomCity(),
                    ZipCode = GenerateRandomNumber(5)
                },
            }).ToList();

            var categories = Enumerable.Range(1, 50).Select(x => new Category
            {
                Name = GenerateRandomCategory(),
                Products = Enumerable.Range(1, 10).Select(x => new Product
                {
                    Name = GenerateRandomProduct(),
                    Price = GenerateRandomDecimal(),
                    Quantity = GenerateRandomInt(1, 100),
                    Tags = Enumerable.Range(1, 3).Select(x => new Tag 
                        {
                            Name = GenerateRandomTag()
                    }).ToList(),
                    Orders = Enumerable.Range(1, 3).Select(x => new Order
                    {
                            OrderDate = GenerateRandomDateTime(),
                            TotalCost = GenerateRandomDecimal(),
                            Client = clients[GenerateRandomInt(1, clients.Count() - 1)],
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
        private string GenerateRandomTag()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string tag = "tag" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return tag;
        }
        private string GenerateRandomStreet()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string street = "ulica" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return street;
        }
        private string GenerateRandomCategory()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string category = "kategoria" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return category;
        }
        private string GenerateRandomProduct()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string product = "produkt" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return product;
        }
        private string GenerateRandomName()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string name = "klient" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return name;
        }
        private string GenerateRandomCity()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string cityName = "miasto" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return cityName;
        }
        private string GenerateRandomSurname()
        {
           
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);

            string surname = "nazwisko" + new string(Enumerable.Range(0, length)
                .Select(_ => suffix[random.Next(suffix.Length)])
                .ToArray());

            return surname;
    
        }
        private string GenerateRandomNumber(int length)
        {
            const string chars = "123456789";
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
