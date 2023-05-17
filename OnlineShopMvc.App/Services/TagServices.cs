using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Services
{
    public class TagServices : ITagService
    {

        private readonly ITagRepo _tagRepo;
        private readonly IMapper _mapper;
        public TagServices(ITagRepo tagRepo, IMapper mapper)
        {
           _tagRepo = tagRepo;
            _mapper = mapper;
        }

        public string AddTag(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa tagu");
            }
            else if (GetTagByName(name) != null)
            {
                throw new ArgumentException("Tag o tej nazwie już istnieje");
            }
            else return _tagRepo.AddTag(name);
        }

        public TagsForListDTO GetAllTags()
        {
            var tags = _tagRepo.GetAllTags().ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToList();
            var tagsDTO = new TagsForListDTO()
            {
               Tags = tags,
               Count = tags.Count
            };

            return tagsDTO;
        }

        public TagDTO GetTagById(int id)
        {
            if (id<=0 || id ==null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator tagu");
            }
            else
            {
                var tag = _tagRepo.GetTagById(id);
                var tagDTO = _mapper.Map<TagDTO>(tag);

                return tagDTO;
            }   
        }
  
        public TagDTO GetTagByName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa tagu");
            }
            else 
            {
                var tag = _tagRepo.GetTagByName(name); ;
                var tagDTO = _mapper.Map<TagDTO>(tag);

                return tagDTO;
            }
        }

        public bool RemoveTag(int id)
        {
            if (id <= 0 || id==null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator tagu");
            }
            return _tagRepo.RemoveTag(id);
        }

        public bool UpdateTag(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowa nazwa tagu");
            }
            else if (GetTagByName(name) != null)
            {
                throw new ArgumentException("Tag o tej nazwie już istnieje");
            }
            return _tagRepo.UpdateTag(name);
        }
    }
}
