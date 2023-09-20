using OnlineShopMvc.App.DTOs.UserDTOs;

namespace OnlineShopMvc.App.Interfaces
{
    public interface IUserService
    {
         UsersForListDTO GetAllUsers(int? pageSize, int? pageNo, string searchName);

         AdminsForListDTO GetAdmins(int? pageSize, int? pageNo, string searchName);

         UserPasswordDTO GetUserPassword(string id);

         bool SetUserPassword(string id, string password);

         bool DeleteUser(string id);

         bool UpdateAdmin(AdminDetailsDTO admin);

         Task<string> AddAdmin(AdminDetailsDTO admin);

         AdminDetailsDTO PrepareModel(string id);
    }
}