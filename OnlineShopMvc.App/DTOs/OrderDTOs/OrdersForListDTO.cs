namespace OnlineShopMvc.App.DTOs.OrderDTOs
{
    public class OrdersForListDTO 
    {
        public List<OrderDTO> Orders { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue {get;set;}
        public int? SortByPrice { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int Count { get; set; }
    }
}
