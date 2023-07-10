using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Interfaces
{
    public interface IClientRepo
    {
        bool RemoveClient(int id);

        string AddClientAndAddress(Client client);

        string UpdateClientAndAddress(Client client);

        IQueryable GetClientsBySurname(string surname);

        Client GetClientById(int id);

        IQueryable ShowAllClients();

        IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city);
    }
}