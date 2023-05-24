using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface ITagRepo
    {
        Tag GetTagById(int? id);
        IQueryable GetAllTags(string? name);
        bool RemoveTag(int? id);
        bool UpdateTag(int id, string? name);
        bool IsTagNameTaken(string? name);
        string AddTag(string name);

    }
}
