using Microsoft.EntityFrameworkCore;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Domain.Interfaces;
using OnlineShopMvc.Inf.Data;
using OnlineShopMVC.Domain.Model;

namespace OnlineShopMvc.Inf.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly Context context;

        public UserRepo(Context context)
        {
            this.context = context;
        }

        public bool DeleteUser(string id)
        {
            try
            {
                var user = GetUserById(id);

                if (user != null && !user.ClientId.HasValue)
                {
                    context.Remove(user.Role);
                    context.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                else if (user != null && user.ClientId.HasValue)
                {
                    var client = GetClientById(user.ClientId.Value);
                    client.IsActive = false;
                    context.Update(client);
                    context.Remove(user.Role);
                    context.Remove(user);
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

        public Client GetClientById(int id)
        {
            return context.Clients.SingleOrDefault(x => x.Id == id);
        }

        public User GetUserById(string id)
        {
            return context.Users.Include(x => x.Role).SingleOrDefault(x => x.Id == id);
        }

        public string AddUser(User user)
        {
            try
            {
                context.Add(user);
                context.SaveChanges();
                return "Udało się dodać użytkownika";
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var userFound = GetUserById(user.Id);
                userFound.Email = user.Email;
                userFound.UserName = user.UserName;
                userFound.PasswordHash = user.PasswordHash;

                context.Update(userFound);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}