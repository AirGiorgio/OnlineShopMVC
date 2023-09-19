using OnlineShopMvc.App.DTOs.UserDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IUserService
    {
        public UsersForListDTO GetAllUsers(int? pageSize, int? pageNo, string searchName);

        public AdminsForListDTO GetAdmins(int? pageSize, int? pageNo, string searchName);

        public UserPasswordDTO GetUserPassword(string id);

        public bool SetUserPassword(string id, string password);

        public bool DeleteUser(string id);

        public bool UpdateAdmin(AdminDetailsDTO admin);

        public string AddAdmin(AdminDetailsDTO admin);

        public AdminDetailsDTO PrepareModel(string id);
    }
}