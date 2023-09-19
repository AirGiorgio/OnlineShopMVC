namespace OnlineShopMvc.App.DTOs.UserDTOs
{
    public class AdminsForListDTO
    {
        public List<AdminDTO> Admins { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }
        public string SearchName { get; set; }
    }
}