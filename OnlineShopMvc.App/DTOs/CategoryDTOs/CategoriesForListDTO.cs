namespace OnlineShopMvc.App.DTOs.CategoryDTOs
{
    public class CategoriesForListDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}