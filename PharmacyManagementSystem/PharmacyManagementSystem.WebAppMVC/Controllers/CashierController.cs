using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class CashierController : Controller
    {
        private SessionHelper _sessionHelper;
        private readonly IApiMedicineServices _apiMedicineServices;
        private readonly IApiSaleServices _apiSaleServices;

        public CashierController(SessionHelper sessionHelper, IApiMedicineServices apiMedicineServices, IApiSaleServices apiSaleServices)
        {
            _sessionHelper = sessionHelper;
            _apiMedicineServices = apiMedicineServices;
            _apiSaleServices = apiSaleServices;
        }

        public IActionResult Dashboard()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                return View();
            }
            return RedirectToAction("Login", "Accounts");
        }

        // Sales Management

        [HttpGet]
        public IActionResult Sales()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                return View();
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> Sales(CreateSaleDto model)
        {
            if (ModelState.IsValid)
            {
                await _apiSaleServices.CreateSaleAsync(model)!;
                if (true)
                {
                    return RedirectToAction("Sales");
                }
            }
            return View(model);
        }

        // Medicine Search

        [HttpGet]
        public IActionResult SearchMedicines()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                var model = new List<GetMedicineDto>();
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> SearchMedicines(string search)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                var model = await _apiMedicineServices.GetMedicineByNameAsync(search);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

    }
}
