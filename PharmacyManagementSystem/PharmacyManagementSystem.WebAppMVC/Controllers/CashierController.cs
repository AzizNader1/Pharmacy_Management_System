using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using System.Collections.Generic;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class CashierController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Sales()
        {
            return View();
        }

        public IActionResult SearchMedicines()
        {
            var model = new List<GetMedicineDto>();
            return View(model);
        }
    }
}
