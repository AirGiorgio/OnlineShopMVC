using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.UserDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Areas.Identity.Data;
using OnlineShopMvc.Domain.Interfaces;
using OnlineShopMvc.Domain.Model;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShopMvc.App.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepo _userRepo;
        private readonly RoleManager<Role> _roleManager;
        public UserServices(IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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
                var adminUsers = _userManager.GetUsersInRoleAsync("Admin").Result;  
                admins = adminUsers.Select(user => _mapper.Map<AdminDTO>(user)).ToList();
            }
            else
            {
                var adminUsers = _userManager.GetUsersInRoleAsync("Admin")
                                   .Result 
                                   .Where(user => user.UserName.StartsWith(searchName));

                admins = adminUsers.Select(user => _mapper.Map<AdminDTO>(user)).ToList();
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
                var Users = _userManager.GetUsersInRoleAsync("User").Result;
                users = Users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
            }
            else
            {
                var Users = _userManager.GetUsersInRoleAsync("User")
                                   .Result
                                   .Where(user => user.UserName.StartsWith(searchName));
                users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
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
                    user.PasswordHash = password;
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

        public async Task<string> AddAdmin(AdminDetailsDTO adminDTO)
        {
            var admin = _mapper.Map<User>(adminDTO);
            admin.PasswordHash = adminDTO.Password;
            admin.Id = Guid.NewGuid().ToString();

            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                var newRole = new Role { Name = "Admin" }; 
                var createRoleResult = await _roleManager.CreateAsync(newRole);
                if (!createRoleResult.Succeeded)
                {
                    return "Error: Unable to create role.";
                }
            }

            var result = await _userManager.AddToRoleAsync(admin, "Admin");

            if (result.Succeeded)
            {
                var addUserResult = _userRepo.AddUser(admin);
                return addUserResult;
            }
            else
            {

                return "Error: Unable to assign role.";
            }
        }

        //private string HashPassword(string password)
        //{
        //    SHA512 hash = SHA512.Create();
        //    var passwordBytes = Encoding.Default.GetBytes(password);
        //    var hashedPassword = hash.ComputeHash(passwordBytes);
        //    return Convert.ToHexString(hashedPassword);
        //}
    }
}