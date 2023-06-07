using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShopMvc.App;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMvc.Inf;
using OnlineShopMVC.Infrastructure;

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

            //builder.Services.AddControllersWithViews().AddFluentValidation(fv=>fv.RunDefaultMvcValidationAfterFluentValidationExecutes==false);
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
           
          

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

            //app.MapControllerRoute(
            //   name: "Clients",
            //   pattern: "{controller=AdminClientController}/{action=ViewClients}");
            // app.MapControllerRoute(
            //      name: "AddClient",
            //      pattern: "{controller=AdminClientController}/{action=AddClient}");
            // app.MapControllerRoute(
            //      name: "AddAddress",
            //      pattern: "{controller=AdminClientController}/{action=AddAddress}");

            // app.MapControllerRoute(
            //      name: "Orders",
            //      pattern: "{controller=AdminOrderController}/{action=ViewOrders}");

            // app.MapControllerRoute(
            //name: "AddTag",
            //pattern: "{controller=AdminTagController}/{action=AddTag}");

            // app.MapControllerRoute(
            // name: "Tags",
            // pattern: "{controller=AdminTagController}/{action=ViewTags}");

            // app.MapControllerRoute(
            // name: "Categories",
            // pattern: "{controller=AdminCategoryController}/{action=ViewCategories}");

            // app.MapControllerRoute(
            //name: "AddCategory",
            //pattern: "{controller=AdminCategoryController}/{action=AddCategory}");

            app.Run();
        }
    }
}