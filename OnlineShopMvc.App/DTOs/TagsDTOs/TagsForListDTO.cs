namespace OnlineShopMvc.App.DTOs.TagsDTOs
{
    public class TagsForListDTO  
    {
        public List<TagDTO> Tags { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
