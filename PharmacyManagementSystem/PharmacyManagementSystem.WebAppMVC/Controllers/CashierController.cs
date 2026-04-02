using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Sales()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Cashier"))
            {
                var userInfo = _sessionHelper.GetUserInfo();
                var saleDto = new CreateSaleDto
                {
                    UserId = userInfo?.UserId ?? 0,
                    SalesDate = DateTime.Now
                };
                return View(saleDto);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> Sales(CreateSaleDto model, List<CreateSaleItemDto> saleItems)
        {
            if (ModelState.IsValid)
            {
                var createdSale = await _apiSaleServices.CreateSaleAsync(model)!;
                if (createdSale != null && createdSale.SaleId != 0)
                {
                    bool itemsSuccess = true;
                    if (saleItems != null && saleItems.Any())
                    {
                        foreach (var item in saleItems)
                        {
                            item.SaleId = createdSale.SaleId;
                            var createdItem = await _apiSaleItemsServices.CreateSaleItemAsync(item)!;
                            if (createdItem == null || createdItem.SaleItemId == 0)
                            {
                                itemsSuccess = false;
                            }
                        }
                    }

                    if (itemsSuccess)
                    {
                        TempData["SuccessMessage"] = "Sale processed successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Sale created, but some items failed to save.";
                    }
                    return RedirectToAction("Sales");
                }
                TempData["ErrorMessage"] = createdSale?.Message ?? "Failed to create sale.";
            }
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
            var medBatch = batches?.FirstOrDefault(b => b.MedicineId == medicineId && b.ExpiryDate > DateTime.Now && b.BatchQuantity > 0);
            var batchId = medBatch != null ? medBatch.BatchId : 0;

            return Json(new
            {
                medicineId = med.MedicineId,
                name = med.Name,
                price = med.MedicinePrice,
                batchId = batchId
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
                    return View(model);

                TempData["ErrorMessage"] = model?.Message ?? "No medicines found.";
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }
    }
}
