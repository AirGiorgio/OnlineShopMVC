using OnlineShopMvc.App.DTOs.CategoryDTOs;
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
        TagProductsDTO GetTagProducts(int id);
        TagDTO GetTagById(int id);  
        TagsForListDTO GetAllTags(int? pageSize, int? pageNo, string? name);  
        bool RemoveTag(int id);
        string UpdateTag(int id, string? name);
        string AddTag(string? name);

    }
}
