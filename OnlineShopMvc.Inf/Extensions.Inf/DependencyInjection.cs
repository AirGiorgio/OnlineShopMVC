using Microsoft.Extensions.DependencyInjection;
using OnlineShopMvc.Domain.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using SteamLibraryMVC.Infrastructure.Repositories;

namespace OnlineShopMvc.Inf.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IOrderRepo, OrderRepo>();
        services.AddTransient<IClientRepo, ClientRepo>();
        services.AddTransient<ICategoryRepo, CategoryRepo>();
        services.AddTransient<IProductRepo, ProductRepo>();
        services.AddTransient<ITagRepo, TagRepo>();
        services.AddTransient<IUserRepo, UserRepo>();

        return services;
    }
}