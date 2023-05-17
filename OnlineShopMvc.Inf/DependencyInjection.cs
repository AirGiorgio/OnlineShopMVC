using Microsoft.Extensions.DependencyInjection;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using SteamLibraryMVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAddressRepo, AddressRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<ITagRepo, TagRepo>();

            return services;
        }
    }
}
