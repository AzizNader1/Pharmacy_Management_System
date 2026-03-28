using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IApiAccountServices _apiAccountServices;
        private SessionHelper _sessionHelper;

        public AccountsController(IApiAccountServices apiAccountServices, SessionHelper sessionHelper)
        {
            _apiAccountServices = apiAccountServices;
            _sessionHelper = sessionHelper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_sessionHelper.IsAuthenticated())
            {
                if (_sessionHelper.IsInRole("Admin"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Cashier");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginResponse = await _apiAccountServices.Login(model);
            if (loginResponse != null && string.Equals(loginResponse.Role.ToString(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Dashboard", "Cashier");
            }

            if (loginResponse != null && string.Equals(loginResponse.Role.ToString(), "Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            ViewBag.ErrorMessage = loginResponse!.Message == string.Empty ? "Invalid login attempt." : loginResponse!.Message;
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (_sessionHelper.IsAuthenticated())
            {
                if (_sessionHelper.IsInRole("Admin"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Cashier");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerResponse = await _apiAccountServices.Register(model);
            if (registerResponse != null && string.Equals(registerResponse.Role.ToString(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Dashboard", "Cashier");
            }

            if (registerResponse != null && string.Equals(registerResponse.Role.ToString(), "Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            ViewBag.ErrorMessage = registerResponse!.Message == string.Empty ? "Invalid login attempt." : registerResponse!.Message;
            return View(model);
        }

        public IActionResult Logout()
        {
            _apiAccountServices.Logout();
            return RedirectToAction("Login");
        }
    }
}
