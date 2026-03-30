using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Models;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Controllers
{
    public class AdminController : Controller
    {
        private SessionHelper _sessionHelper;
        private readonly IApiBatchServices _batchService;
        private readonly IApiMedicineServices _medicineService;
        private readonly IApiSaleServices _saleService;
        private readonly IApiSaleItemsServices _saleItemService;
        private readonly IApiUserServices _userService;

        public AdminController(SessionHelper sessionHelper, IApiBatchServices batchService, IApiMedicineServices medicineService, IApiSaleServices saleService, IApiSaleItemsServices saleItemService, IApiUserServices userService)
        {
            _sessionHelper = sessionHelper;
            _batchService = batchService;
            _medicineService = medicineService;
            _saleService = saleService;
            _saleItemService = saleItemService;
            _userService = userService;
        }

        public IActionResult Dashboard()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var adminViewModel = new AdminViewModel();
                return View(adminViewModel);
            }
            return RedirectToAction("Login", "Accounts");
        }

        // Medicine Management

        [HttpGet]
        public async Task<IActionResult> ManageMedicines()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _medicineService.GetAllMedicinesAsync();
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public async Task<IActionResult> EditMedicine(int id)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _medicineService.GetMedicineByIdAsync(id);
                ViewBag.MedicineId = id;
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicine(int id, UpdateMedicineDto model)
        {
            if (ModelState.IsValid)
            {
                await _medicineService.UpdateMedicineAsync(id, model)!;
                if (true)
                {
                    return RedirectToAction("ManageMedicines");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _medicineService.GetMedicineByIdAsync(id);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicine(int id, GetMedicineDto getMedicineDto)
        {
            await _medicineService.DeleteMedicineAsync(id)!;

            return View();
        }

        [HttpGet]
        public IActionResult CreateMedicine()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                return View(new CreateMedicineDto());
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDto model)
        {
            if (ModelState.IsValid)
            {
                await _medicineService.CreateMedicineAsync(model)!;
                if (true)
                {
                    return RedirectToAction("ManageMedicines");
                }
            }
            return View(model);
        }

        // Batch Management

        [HttpGet]
        public async Task<IActionResult> ManageBatchs()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _batchService.GetAllBatchesAsync();
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public async Task<IActionResult> EditBatch(int id)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _batchService.GetBatchByIdAsync(id);
                ViewBag.BatchId = id;
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> EditBatch(int id, UpdateBatchDto model)
        {
            if (ModelState.IsValid)
            {
                await _batchService.UpdateBatchAsync(id, model)!;
                if (true)
                {
                    return RedirectToAction("ManageBatches");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _batchService.GetBatchByIdAsync(id);
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBatch(int id, GetBatchDto getBatchDto)
        {
            await _batchService.DeleteBatchAsync(id)!;

            return View();
        }

        [HttpGet]
        public IActionResult CreateBatch()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                return View(new CreateBatchDto());
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBatch(CreateBatchDto model)
        {
            if (ModelState.IsValid)
            {
                await _batchService.CreateBatchAsync(model)!;
                if (true)
                {
                    return RedirectToAction("ManageBatches");
                }
            }
            return View(model);
        }

        // Sale Management

        [HttpGet]
        public async Task<IActionResult> ManageSales()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _saleService.GetAllSalesAsync();
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        // Users Management

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var model = await _userService.GetAllUsersAsync();
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        public IActionResult Reports()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Accounts");
        }
    }
}
