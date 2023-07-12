using FluentValidation;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using static OnlineShopMvc.App.DTOs.ClientDTOs.ClientDetailsDTO;
using static OnlineShopMvc.App.DTOs.TagsDTOs.TagDTO;

namespace OnlineShopMvc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CategoryDTO>, CategoryValidation>();
            services.AddTransient<IValidator<TagDTO>, TagValidation>();
            services.AddTransient<IValidator<ClientDetailsDTO>, ClientValidation>();
            services.AddTransient<IValidator<ProductDetailsDTO>, ProductValidation>();

            return services;
        }
    }
}