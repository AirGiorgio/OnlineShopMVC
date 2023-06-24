namespace OnlineShopMVC.Domain.Model
{
    public class Address
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public virtual Client Client { get; set; }
    }
}
