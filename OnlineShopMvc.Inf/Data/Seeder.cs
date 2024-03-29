﻿using Microsoft.AspNetCore.Identity;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Domain.Model;
using OnlineShopMVC.Domain.Model;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShopMvc.Inf.Data
{
    public class Seeder
    {
        private readonly Context _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public Seeder(Context context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (_context.Clients.Any())
                {
                    return;
                }
                else
                {
                    InitializeRoles(_roleManager);
                    CreateUsers(_userManager);
                    var Clients = _context.Clients.ToList(); 
                    GetCategories(Clients);
                    _context.Categories.AddRange(Categories);
                    _context.SaveChanges();
                }
            }
        }

        private List<Category> Categories;
        
        public void InitializeRoles(RoleManager<Role> roleManager)
        {

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;

                if (!roleExist)
                {
                    var role = new Role
                    {
                        Name = roleName
                    };

                    var createResult = roleManager.CreateAsync(role).Result;
                }
            }
        }

        public void CreateUsers(UserManager<User> userManager)
        {
            for (int i = 1; i <= 100; i++)
            {
                var userName = $"user{i}";
                var password = GenerateRandomString();
                var roleName = i % 2 == 0 ? "Admin" : "User";

                var user = new User
                {
                    UserName = userName,
                    Email = $"{userName}@example.com",
                };

                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRoleAsync(user, roleName).Result;

                    if (roleName == "User")
                    {
                        user.Client = new Client
                        {
                            Name = GenerateRandomName(),
                            Surname = GenerateRandomSurname(),
                            EmailAdress = GenerateRandomEmail(),
                            Telephone = GenerateRandomNumber(10),
                            IsActive = true,
                            Address = new Address
                            {
                                Street = GenerateRandomStreet(),
                                BuildingNumber = GenerateRandomNumber(1),
                                FlatNumber = GenerateRandomNumber(1),
                                City = GenerateRandomCity(),
                                ZipCode = GenerateRandomNumber(5)
                            },
                        };

                        _context.Clients.Add(user.Client);
                        _context.SaveChanges();
                    }
                }
            }
        }
           
         private List<Category> GetCategories(List<Client> Clients)
         {
            Categories = Enumerable.Range(1, 50).Select(x => new Category
            {
                Name = GenerateRandomCategory(),
                Products = Enumerable.Range(1, 10).Select(x => new Product
                {
                    Name = GenerateRandomProduct(),
                    Price = GenerateRandomDecimal(),
                    Quantity = GenerateRandomInt(1, 100),
                    IsActive = true,
                    Tags = Enumerable.Range(1, 3).Select(x => new Tag
                    {
                        Name = GenerateRandomTag()
                    }).ToList(),
                    OrderProducts = Enumerable.Range(1, 10).Select(x => new OrderProduct
                    {
                        Amount = GenerateRandomInt(1, 5),
                        Order = new Order()
                        {
                            OrderId = GenerateRandomString(),
                            OrderDate = GenerateRandomDateTime(),
                            TotalCost = GenerateRandomDecimal(),
                            Client = Clients[GenerateRandomInt(1, Clients.Count() - 1)]
                        }
                    }).ToList(),
                }).ToList(),
            }).ToList();
            return Categories;
         }

        private string GenerateRandomEmail()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            int length = random.Next(3, 9);
            string user = "User" + new string(Enumerable.Range(0, length)
              .Select(_ => suffix[random.Next(suffix.Length)])
              .ToArray()) + "@przyklad.com";

            return user;
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

        private string GenerateRandomString()
        {
            const string suffix = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialCharacters = "!@#$%^&*()_+[]{}|;:,.<>?";
            const string digits = "0123456789";
            

            var random = new Random();
            int length = random.Next(12, 12);

            string randomString = new string(Enumerable.Range(0, length - 3)
            .Select(_ => suffix[random.Next(suffix.Length)])
            .Concat(new[]
            {
                    char.ToUpper(suffix[random.Next(suffix.Length)]),  
                    digits[random.Next(digits.Length)],
                    specialCharacters[random.Next(specialCharacters.Length)]
            })
            .ToArray());

            return randomString;


        }

        //private string HashPassword(string password)
        //{
        //    SHA512 hash = SHA512.Create();
        //    var passwordBytes = Encoding.Default.GetBytes(password);
        //    var hashedPassword = hash.ComputeHash(passwordBytes);
        //    return Convert.ToHexString(hashedPassword);
        //}
    }
}