using AutoMapper;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.Mapping;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.DTOs.TagsDTOs
{
    public class TagsForListDTO :IMapFrom<Tag>
    {
        public List<TagDTO> Tags { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
