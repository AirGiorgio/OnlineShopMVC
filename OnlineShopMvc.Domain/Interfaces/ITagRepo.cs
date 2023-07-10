using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface ITagRepo
    {
        Tag GetTagById(int? id);

        IQueryable GetAllTags(string? name);

        bool RemoveTag(int? id);

        string UpdateTag(int id, string? name);

        bool IsTagNameTaken(string? name);

        string AddTag(string name);
    }
}