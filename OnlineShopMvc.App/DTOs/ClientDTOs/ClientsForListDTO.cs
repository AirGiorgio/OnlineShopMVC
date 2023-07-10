namespace OnlineShopMvc.App.DTOs.ClientDTOs;

public class ClientsForListDTO 
    {
        public List<ClientDTO> Clients { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchName { get; set; }
        public string SearchCity { get; set; }
        public string SearchBuilding { get; set; }
        public string SearchStreet { get; set; }
        public int Count { get; set; }
    }

