using OnlineShopMvc.App.DTOs.TagsDTOs;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ITagService
    {
        TagDTO GetTagById(int id);  
        TagDTO GetTagByName(string? name);   
        TagsForListDTO GetAllTags();  
        bool RemoveTag(int id);
        bool UpdateTag(string? name);
        string AddTag(string? name);

    }
}
