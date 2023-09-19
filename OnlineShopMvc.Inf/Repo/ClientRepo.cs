using Microsoft.EntityFrameworkCore;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Inf.Data;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;

namespace SteamLibraryMVC.Infrastructure.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly Context context;

        public ClientRepo(Context context)
        {
            this.context = context;
        }

        public bool RemoveClient(int id)
        {
            try
            {
                var client = GetClientById(id);

                if (client != null)
                {
                    var user = GetUserByClientId(id);
                    client.IsActive = false;
                    user.ClientId = null;
                    context.Update(client);
                    context.Update(user);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable GetClientsBySurname(string surname)
        {
            return context.Clients.Where(i => i.Surname.StartsWith(surname) && i.IsActive == true);
        }

        public Client GetClientById(int id)
        {
            return context.Clients.Include(x => x.Address).SingleOrDefault(x => x.Id == id);
        }

        public User GetUserByClientId(int id)
        {
            return context.Users.SingleOrDefault(x => x.ClientId == id);
        }

        public IQueryable ShowAllClients()
        {
            return context.Clients.Include(x => x.User).Where(i => i.IsActive == true);
        }

        public IQueryable GetClientByUserName(string name)
        {
            return context.Clients.Include(x => x.User).Where(i => i.User.UserName.StartsWith(name));
        }

        public string AddClientAndAddress(Client client)
        {
            try
            {
                context.Add(client);
                context.SaveChanges();
                return "Rejestracja udana";
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public string UpdateClientAndAddress(Client client)
        {
            try
            {
                var clientF = GetClientById(client.Id);
                if (clientF == null)
                {
                    return "Nie udało się znaleźć klienta";
                }
                clientF.Name = client.Name;
                clientF.Surname = client.Surname;
                clientF.Telephone = client.Telephone;
                clientF.EmailAdress = client.EmailAdress;
                clientF.Address = client.Address;
                context.SaveChanges();
                return "Udało się zaktualizować klienta";
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public IQueryable GetClientByStreetName(string? street, string? buildingNumber, string? city)
        {
            IQueryable<Client> query = context.Clients.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(buildingNumber) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.BuildingNumber.StartsWith(buildingNumber) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(buildingNumber))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.BuildingNumber.StartsWith(buildingNumber));
            }
            else if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.Street.StartsWith(street) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(buildingNumber) && !string.IsNullOrEmpty(city))
            {
                query = query.Where(c =>
                    c.Address.BuildingNumber.StartsWith(buildingNumber) &&
                    c.Address.City.StartsWith(city));
            }
            else if (!string.IsNullOrEmpty(street))
            {
                query = query.Where(c => c.Address.Street.StartsWith(street));
            }
            else if (!string.IsNullOrEmpty(buildingNumber))
            {
                query = query.Where(c => c.Address.BuildingNumber.StartsWith(buildingNumber));
            }
            else if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.Address.City.StartsWith(city));
            }
            else
            {
                query = context.Clients.Where(x => x.IsActive == true);
            }
            return query;
        }
    }
}