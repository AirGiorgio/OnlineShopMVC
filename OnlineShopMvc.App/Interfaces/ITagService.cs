using OnlineShopMvc.App.DTOs.TagsDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ITagService
    {
        public TagDTO PrepareModel();
        TagProductsDTO GetTagProducts(int id);
        TagDTO GetTagById(int id);  
        TagsForListDTO GetAllTags(int? pageSize, int? pageNo, string? name);  
        bool RemoveTag(int id);
        string UpdateTag(int id, string? name);
        string AddTag(string? name);

    }
}
