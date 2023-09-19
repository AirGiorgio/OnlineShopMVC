using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.Mapping;
using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class UserPasswordDTO : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string ConfirmPassword { get; set; }

        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserPasswordDTO>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName)).ReverseMap();
        }
    }

    public class UserValidation : AbstractValidator<UserPasswordDTO>
    {
        public UserValidation()
        {
            RuleFor(x => x.Password).NotNull().MinimumLength(10)
               .Matches(@"[A-Z]").WithMessage("Hasło musi zawierać przynajmniej jedną dużą literę")
                                .Matches(@"[a-z]").WithMessage("Hasło musi zawierać przynajmniej jedną małą literę")
                                .Matches(@"\d").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę")
                                .Matches(@"[!@#$%^&*]").WithMessage("Hasło musi zawierać przynajmniej jednen znak specjalny");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}