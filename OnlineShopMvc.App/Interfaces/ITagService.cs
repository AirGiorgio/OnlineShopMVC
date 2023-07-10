using OnlineShopMvc.App.DTOs.TagsDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface ITagService
    {
        public TagDTO PrepareModel();

        public TagProductsDTO GetTagProducts(int id);

        public TagDTO GetTagById(int id);

        public TagsForListDTO GetAllTags(int? pageSize, int? pageNo, string? name);

        public bool RemoveTag(int id);

        public string UpdateTag(int id, string? name);

        public string AddTag(string? name);
    }
}