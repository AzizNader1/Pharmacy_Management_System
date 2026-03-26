using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName?.ToLower() == "admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Cashier");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
