using Microsoft.Extensions.DependencyInjection;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.App.Services;
using OnlineShopMvc.Inf.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IClientService, ClientServices>();
            services.AddTransient<IOrderService, OrderServices>();
            services.AddTransient<ICategoryService, CategoryServices>();
            services.AddTransient<IProductService, ProductServices>();
            services.AddTransient<ITagService, TagServices>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());    
            return services;
        }
    }
}
