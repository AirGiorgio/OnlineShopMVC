using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineShopMvc.App.DTOs.UserDTOs;
using OnlineShopMvc.App.Interfaces;

namespace OnlineShopMvc.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly ILogger<AdminUserController> _logger;
        private readonly IUserService _userService;
        private readonly IValidator<UserPasswordDTO> _userValidator;
        private readonly IValidator<AdminDetailsDTO> _adminValidator;

        public AdminUserController(ILogger<AdminUserController> logger, IUserService userService, IValidator<UserPasswordDTO> userValidator, IValidator<AdminDetailsDTO> adminValidator)
        {
            _userValidator = userValidator;
            _logger = logger;
            _userService = userService;
            _adminValidator = adminValidator;
        }

        [HttpGet]
        public IActionResult AdminDetails(string id)
        {
            var user = _userService.GetUserPassword(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult UserDetails(string id)
        {
            var user = _userService.GetUserPassword(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult ResetAdminPassword(UserPasswordDTO user)
        {
            var result = _userValidator.Validate(user);
            if (!result.IsValid)
            {
                return View("AdminDetails", user);
            }
            var status = _userService.SetUserPassword(user.Id, user.Password);
            if (status == false)
            {
                TempData["Message"] = "Nie udało się zmienić hasła użytkownika";
            }
            else
            {
                TempData["Message"] = "Udało się zmienić hasło użytkownika";
            }
            return RedirectToAction("ViewAdmins", "AdminUser");
        }

        [HttpPost]
        public IActionResult ResetUserPassword(UserPasswordDTO user)
        {
            var result = _userValidator.Validate(user);

            if (!result.IsValid)
            {
                return View("UserDetails", user);
            }
            var status = _userService.SetUserPassword(user.Id, user.Password);
            if (status == false)
            {
                TempData["Message"] = "Nie udało się zmienić hasła użytkownika";
            }
            else
            {
                TempData["Message"] = "Udało się zmienić hasło użytkownika";
            }
            return RedirectToAction("ViewUsers", "AdminUser");
        }

        [HttpPost]
        public IActionResult DeleteAdmin(string id)
        {
            var status = _userService.DeleteUser(id);
            if (status == false)
            {
                TempData["Message"] = "Użytkownik już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto użytkownika";
            }
            return RedirectToAction("ViewAdmins", "AdminUser");
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            var status = _userService.DeleteUser(id);
            if (status == false)
            {
                TempData["Message"] = "Użytkownik już nie istnieje";
            }
            else
            {
                TempData["Message"] = "Usunięto użytkownika";
            }
            return RedirectToAction("ViewUsers", "AdminUser");
        }

        [HttpGet]
        public IActionResult ViewAdmins(int? pageSize, int? pageNo, string searchName)
        {
            var admins = _userService.GetAdmins(pageSize, pageNo, searchName);
            return View(admins);
        }

        [HttpGet]
        public IActionResult ViewUsers(int? pageSize, int? pageNo, string searchname)
        {
            var users = _userService.GetAllUsers(pageSize, pageNo, searchname);
            return View(users);
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            AdminDetailsDTO adminDetailsDTO = new AdminDetailsDTO();
            return View(adminDetailsDTO);
        }

        [HttpPost]
        public IActionResult AddAdmin(AdminDetailsDTO admin)
        {
            var result = _adminValidator.Validate(admin);

            if (!result.IsValid)
            {
                return View("AddAdmin", admin);
            }
            var status = _userService.AddAdmin(admin);
            TempData["Message"] = status;
            return RedirectToAction("ViewAdmins", "AdminUser");
        }

        [HttpPost]
        public IActionResult UpdateAdmin(AdminDetailsDTO admin)
        {
            var result = _adminValidator.Validate(admin);

            if (!result.IsValid)
            {
                return View("UpdateAdmin", admin);
            }
            var status = _userService.UpdateAdmin(admin);
            if (status == false)
            {
                TempData["Message"] = "Nie udało się zaktualizować użytkownika";
            }
            else
            {
                TempData["Message"] = "Pomyślnie zaktualizowano użytkownika";
            }
            return RedirectToAction("ViewAdmins", "AdminUser");
        }

        [HttpGet]
        public IActionResult UpdateAdmin(string id)
        {
            var admin = _userService.PrepareModel(id);
            return View(admin);
        }
    }
}