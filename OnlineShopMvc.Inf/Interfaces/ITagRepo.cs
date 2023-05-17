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
        Tag GetTagByName(string? name);
        IQueryable GetAllTags();
        bool RemoveTag(int? id);
        bool UpdateTag(string? name);
        string AddTag(string name);

    }
}
