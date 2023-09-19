using FluentValidation;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.DTOs.UserDTOs;
using static OnlineShopMvc.App.DTOs.ClientDTOs.ClientDetailsDTO;
using static OnlineShopMvc.App.DTOs.TagsDTOs.TagDTO;
using static OnlineShopMvc.App.DTOs.UserDTOs.AdminDetailsDTO;

namespace OnlineShopMvc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CategoryDTO>, CategoryValidation>();
            services.AddScoped<IValidator<TagDTO>, TagValidation>();
            services.AddScoped<IValidator<ClientDetailsDTO>, ClientValidation>();
            services.AddScoped<IValidator<ProductDetailsDTO>, ProductValidation>();
            services.AddScoped<IValidator<UserPasswordDTO>, UserValidation>();
            services.AddScoped<IValidator<AdminDetailsDTO>, AdminValidation>();

            return services;
        }
    }
}