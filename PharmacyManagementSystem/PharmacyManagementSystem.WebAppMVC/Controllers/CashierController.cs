using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class CashierController : Controller
    {
        private SessionHelper _sessionHelper;
        private readonly IApiMedicineServices _apiMedicineServices;
        private readonly IApiSaleServices _apiSaleServices;
        private readonly IApiSaleItemsServices _apiSaleItemsServices;
        private readonly IApiBatchServices _apiBatchServices;

        public CashierController(SessionHelper sessionHelper, IApiMedicineServices apiMedicineServices, IApiSaleServices apiSaleServices, IApiSaleItemsServices apiSaleItemsServices, IApiBatchServices apiBatchServices)
        {
            _sessionHelper = sessionHelper;
            _apiMedicineServices = apiMedicineServices;
            _apiSaleServices = apiSaleServices;
            _apiSaleItemsServices = apiSaleItemsServices;
            _apiBatchServices = apiBatchServices;
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
        public async Task<IActionResult> Sales()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                var userInfo = _sessionHelper.GetUserInfo();
                var saleDto = new CreateSaleDto
                {
                    // user id in the create sale dto is realted to the user (cashier) who make the sale, so we will get the user id from the session and pass it to the view
                    UserId = userInfo?.UserId ?? 0,
                    SalesDate = DateTime.Now
                };

                var medicines = await _apiMedicineServices.GetAllMedicinesAsync();
                ViewBag.Medicines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(medicines ?? Enumerable.Empty<GetMedicineDto>(), "MedicineId", "Name");

                return View(saleDto);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> Sales(CreateSaleDto model, List<CreateSaleItemDto> saleItems)
        {
            if (ModelState.IsValid)
            {
                // Step 1: Create the sale
                var createdSale = await _apiSaleServices.CreateSaleWithItemsAsync(model, saleItems)!;
                if (createdSale == null || createdSale.SaleId == 0)
                {
                    TempData["ErrorMessage"] = createdSale?.Message ?? "Failed to create the sale. Please try again.";
                    return RedirectToAction("Sales");
                }
                TempData["SuccessMessage"] = "Checkout completed successfully! Sale has been recorded and inventory updated.";

                return RedirectToAction("Sales");
            }

            // ModelState invalid – reload the view with the form
            var medicines = await _apiMedicineServices.GetAllMedicinesAsync();
            ViewBag.Medicines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(medicines! ?? Enumerable.Empty<GetMedicineDto>(), "MedicineId", "Name");
            return View(model);
        }


        // API Endpoint for POS
        [HttpGet]
        public async Task<IActionResult> GetMedicineForPOS(int medicineId)
        {
            var med = await _apiMedicineServices.GetMedicineByIdAsync(medicineId);
            if (med == null || med.MedicineId == 0)
                return NotFound();

            var batches = await _apiBatchServices.GetAllBatchesAsync();
            var medBatch = batches?.FirstOrDefault(b => b!.MedicineId == medicineId && b.ExpiryDate > DateTime.Now && b.BatchQuantity > 0);
            var batchId = medBatch != null ? medBatch.BatchId : 0;
            var availableQty = medBatch != null ? medBatch.BatchQuantity : 0;

            return Json(new
            {
                medicineId = med.MedicineId,
                name = med.Name,
                price = med.MedicinePrice,
                batchId = batchId,
                availableQty = availableQty
            });
        }

        // Medicine Search

        [HttpGet]
        public async Task<IActionResult> SearchMedicines()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                var model = await _apiMedicineServices.GetAllMedicinesAsync();
                if (model != null && model.FirstOrDefault()!.Message == "")
                    return View(model);

                ViewBag.ErrorMessage = model?.FirstOrDefault()?.Message;
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> SearchMedicines(string search)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    var allMeds = await _apiMedicineServices.GetAllMedicinesAsync();
                    return View(allMeds);
                }

                var model = await _apiMedicineServices.GetMedicineByNameAsync(search);
                if (model != null && model.Message == "")
                {
                    List<GetMedicineDto> medicines = new List<GetMedicineDto> { model };
                    return View(medicines);
                }


                TempData["ErrorMessage"] = model?.Message ?? "No medicines found.";
                return View(new List<GetMedicineDto>());
            }
            return RedirectToAction("Login", "Accounts");
        }
    }
}
