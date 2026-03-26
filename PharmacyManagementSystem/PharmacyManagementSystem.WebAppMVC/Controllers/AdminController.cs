using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using System.Collections.Generic;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ManageMedicines()
        {
            var model = new List<GetMedicineDto>();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateMedicine()
        {
            return View(new CreateMedicineDto());
        }

        [HttpPost]
        public IActionResult CreateMedicine(CreateMedicineDto model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageMedicines");
            }
            return View(model);
        }

        public IActionResult Reports()
        {
            return View();
        }
    }
}
