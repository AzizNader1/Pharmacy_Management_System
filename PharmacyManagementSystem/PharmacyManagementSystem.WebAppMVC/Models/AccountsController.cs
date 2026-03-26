using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagementSystem.WebAppMVC.Models
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
