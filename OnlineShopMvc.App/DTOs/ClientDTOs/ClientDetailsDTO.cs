using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.ClientDTOs;

public class ClientDetailsDTO : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAdress { get; set; }
        public string Telephone { get; set; }
        public AddressDTO Address { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDetailsDTO>().ReverseMap()
                .ForMember(x => x.Address, opt => opt.MapFrom(s => s.Address));
        }

        public class ClientValidation : AbstractValidator<ClientDetailsDTO>
        {
            public ClientValidation()
            {
                RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1).Matches("^[a-zA-Z]+$");
                RuleFor(x => x.Surname).MaximumLength(255).MinimumLength(1).Matches("^[a-zA-Z]+$");
                RuleFor(x => x.Telephone).Length(9).Matches("^[0-9]+$");
                RuleFor(x => x.EmailAdress).NotEmpty().EmailAddress();
                RuleFor(x => x.Address.Street).MaximumLength(255).MinimumLength(1).Matches("^[a-zA-Z]+$");
                RuleFor(x => x.Address.BuildingNumber).MaximumLength(255).MinimumLength(1).Matches("^[0-9]+$");
                RuleFor(x=>x.Address.FlatNumber).MaximumLength(255).MinimumLength(1).Matches("^[0-9]+$");
                RuleFor(x=>x.Address.City).MaximumLength(255).MinimumLength(1).Matches("^[a-zA-Z]+$");
                RuleFor(x=>x.Address.ZipCode).Length(5).Matches("^[0-9]+$");
            }
        }
    }

