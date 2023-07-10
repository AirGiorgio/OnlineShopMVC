using OnlineShopMvc.App.DTOs.ClientDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IClientService
    {
        string UpdateClientAndAddress(ClientDetailsDTO client);

        bool RemoveClient(int id);

        ClientDetailsDTO GetClientById(int id);

        ClientsForListDTO ShowAllClients(int? pageSize, int? pageNo, string? street, string? buildingNumber, string? city, string? surname);

        string AddClientAndAddress(ClientDetailsDTO client);
    }
}