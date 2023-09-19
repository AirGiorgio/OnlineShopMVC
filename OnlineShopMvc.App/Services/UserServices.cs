using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.UserDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShopMvc.App.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepo _userRepo;

        public UserServices(IMapper mapper, UserManager<User> userManager, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public bool DeleteUser(string id)
        {
            if (id == null)
            {
                return false;
            }

            var status = _userRepo.DeleteUser(id);
            return status;
        }

        public AdminsForListDTO GetAdmins(int? pageSize, int? pageNo, string searchName)
        {
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            List<AdminDTO> admins = new List<AdminDTO>();

            if (searchName.IsNullOrEmpty())
            {
                admins = _userManager.Users.Where(x => x.Role.Name == "Admin").ProjectTo<AdminDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                admins = _userManager.Users.Where(x => x.Role.Name == "Admin" && x.UserName.StartsWith(searchName)).ProjectTo<AdminDTO>(_mapper.ConfigurationProvider).ToList();
            }
            var adminsToShow = admins.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();

            var AdminsForListDTO = new AdminsForListDTO()
            {
                Admins = adminsToShow,
                PageNum = pageNo.Value,
                PageSize = pageSize.Value,
                SearchName = searchName,
                Count = admins.Count
            };
            return AdminsForListDTO;
        }

        public UsersForListDTO GetAllUsers(int? pageSize, int? pageNo, string searchName)
        {
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            List<UserDTO> users = new List<UserDTO>();

            if (searchName.IsNullOrEmpty())
            {
                users = _userManager.Users.Where(x => x.Role.Name == "User").ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                users = _userManager.Users.Where(x => x.Role.Name == "User" && x.UserName.StartsWith(searchName)).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToList();
            }

            var usersToShow = users.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();

            var UsersForListDTO = new UsersForListDTO()
            {
                Users = usersToShow,
                PageNum = pageNo.Value,
                PageSize = pageSize.Value,
                SearchName = searchName,
                Count = users.Count
            };
            return UsersForListDTO;
        }

        public UserPasswordDTO GetUserPassword(string id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                var user = _userRepo.GetUserById(id);
                var userPasswordDTO = _mapper.Map<UserPasswordDTO>(user);

                return userPasswordDTO;
            }
        }

        public AdminDetailsDTO PrepareModel(string id)
        {
            var admin = _userRepo.GetUserById(id);
            var adminDTO = _mapper.Map<AdminDetailsDTO>(admin);
            return adminDTO;
        }

        public bool SetUserPassword(string id, string password)
        {
            if (!password.IsNullOrEmpty() || id.IsNullOrEmpty())
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user != null)
                {
                    user.PasswordHash = HashPassword(password);
                    var status = _userRepo.UpdateUser(user);
                    return status;
                }
                else return false;
            }
            else return false;
        }

        public bool UpdateAdmin(AdminDetailsDTO adminDTO)
        {
            var admin = _mapper.Map<User>(adminDTO);
            var result = _userRepo.UpdateUser(admin);
            return result;
        }

        public string AddAdmin(AdminDetailsDTO adminDTO)
        {
            var admin = _mapper.Map<User>(adminDTO);
            admin.PasswordHash = HashPassword(adminDTO.Password);
            admin.Id = Guid.NewGuid().ToString();
            admin.Role = new Domain.Model.Role();
            admin.Role.Name = "Admin";
            var result = _userRepo.AddUser(admin);

            return result;
        }

        private string HashPassword(string password)
        {
            SHA512 hash = SHA512.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }
    }
}