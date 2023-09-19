using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.DTOs.RoleDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class AdminDetailsDTO : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public RoleDTO Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AdminDetailsDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(x => x.Email, opt => opt.MapFrom(s => s.Email))
                 .ForMember(x => x.Email, opt => opt.MapFrom(s => s.Email))
                 .ReverseMap();
        }

        public class AdminValidation : AbstractValidator<AdminDetailsDTO>
        {
            public AdminValidation()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.UserName).MaximumLength(255).MinimumLength(1).Matches("^[a-zA-Z]+$");
                RuleFor(x => x.Password).NotNull().MinimumLength(10)
                   .Matches(@"[A-Z]").WithMessage("Hasło musi zawierać przynajmniej jedną dużą literę")
                                    .Matches(@"[a-z]").WithMessage("Hasło musi zawierać przynajmniej jedną małą literę")
                                    .Matches(@"\d").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę")
                                    .Matches(@"[!@#$%^&*]").WithMessage("Hasło musi zawierać przynajmniej jednen znak specjalny");
                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            }
        }
    }
}