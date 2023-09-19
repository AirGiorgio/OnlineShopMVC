using OnlineShopMvc.App.DTOs.ClientDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IClientService
    {
        public string UpdateClientAndAddress(ClientDetailsDTO client);

        public bool RemoveClient(int id);

        public ClientDetailsDTO GetClientById(int id);

        public ClientsForListDTO ShowAllClients(int? pageSize, int? pageNo, string? street, string? buildingNumber, string? city, string? surname, string? username);

        public string AddClientAndAddress(ClientDetailsDTO client);
    }
}