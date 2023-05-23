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
        TagsForListDTO GetAllTags(int pagesize, int pageno, string? name);  
        bool RemoveTag(int id);
        bool UpdateTag(int id, string? name);
        string AddTag(string? name);

    }
}
