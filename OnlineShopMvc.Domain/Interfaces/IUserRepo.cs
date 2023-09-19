using OnlineShopMvc.Areas.Identity.Data;

namespace OnlineShopMvc.Domain.Interfaces
{
    public interface IUserRepo
    {
        public string AddUser(User user);

        public bool DeleteUser(string id);

        public bool UpdateUser(User user);

        public User GetUserById(string id);
    }
}