using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShopMvc.App;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMvc.Inf;
using OnlineShopMVC.Infrastructure;
using static OnlineShopMvc.App.DTOs.ClientDTOs.ClientDetailsDTO;

namespace OnlineShopMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>();
            builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddMvc();
            //builder.Services.AddControllersWithViews().AddFluentValidation();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddTransient<IValidator<CategoryDTO>, CategoryValidation>();
            builder.Services.AddTransient<IValidator<ClientDetailsDTO>, ClientValidation>();
           

            var app = builder.Build();

            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/myLog-{Date}.txt");

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                context.Database.EnsureCreated();
                Seeder seeder = new Seeder(context);
                seeder.BreedTheSeedAndNeedForSpeed();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "HomeController",
                pattern: "{controller=Home}/{action=Index}/{id?}");
          
            app.Run();
        }
    }
}