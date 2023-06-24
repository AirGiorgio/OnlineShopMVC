using AutoMapper;
using FluentValidation;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.App.DTOs.TagsDTOs
{
    public class TagDTO : IMapFrom<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tag, TagDTO>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name));
        }
        public class TagValidation : AbstractValidator<TagDTO>
        {
            public TagValidation()
            {
                RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1);
            }
        }
    }
}
