using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
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
        private readonly IApiUserServices _userService;
        private readonly IReportsService _reportsService;

        public AdminController(
            SessionHelper sessionHelper,
            IApiBatchServices batchService,
            IApiMedicineServices medicineService,
            IApiSaleServices saleService,
            IApiUserServices userService,
            IReportsService reportsService)
        {
            _sessionHelper = sessionHelper;
            _batchService = batchService;
            _medicineService = medicineService;
            _saleService = saleService;
            _userService = userService;
            _reportsService = reportsService;
        }

        public async Task<IActionResult> Dashboard()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var adminViewModel = new AdminViewModel();
                var batchs = await _batchService.GetAllBatchesAsync();
                adminViewModel.Batchs = batchs.Count() == 0 ? [] : batchs;

                var medicines = await _medicineService.GetAllMedicinesAsync();
                adminViewModel.Medicines = medicines.Count() == 0 ? [] : medicines;

                var sales = await _saleService.GetAllSalesAsync();
                adminViewModel.Sales = sales.Count() == 0 ? [] : sales;

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
                if (model != null && model.FirstOrDefault()!.Message == "")
                    return View(model);

                TempData["ErrorMessage"] = model!.FirstOrDefault()!.Message;
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
                var medicineModel = new UpdateMedicineDto
                {
                    Category = model.Category,
                    Description = model.Description,
                    GenericName = model.GenericName,
                    MedicinePrice = model.MedicinePrice,
                    Name = model.Name,
                    ReorderLevel = model.ReorderLevel
                };
                return View(medicineModel);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicine(int id, UpdateMedicineDto model)
        {
            if (ModelState.IsValid)
            {
                var newData = await _medicineService.UpdateMedicineAsync(id, model)!;
                if (newData != null)
                {
                    TempData["SuccessMessage"] = "Update medicine date done successfully!";
                    return RedirectToAction("ManageMedicines");
                }

                TempData["ErrorMessage"] = newData!.Message;
                return RedirectToAction("ManageMedicines");
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
            var result = await _medicineService.DeleteMedicineAsync(id)!;
            if (!result)
            {
                TempData["ErrorMessage"] = "Delete an existing medicine faild, please try again later";
                return RedirectToAction("ManageMedicines");
            }

            TempData["SuccessMessage"] = "Delete existing medicine done successfully!";
            return RedirectToAction("ManageMedicines");
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
                var createdMedicine = await _medicineService.CreateMedicineAsync(model)!;
                if (createdMedicine.MedicineId != 0)
                {
                    TempData["SuccessMessage"] = "Adding a new medicine done successfully!";
                    return RedirectToAction("ManageMedicines");
                }

                TempData["ErrorMessage"] = createdMedicine!.Message;
                return RedirectToAction("ManageMedicines");
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
                if (model != null && model.FirstOrDefault()!.Message == "")
                    return View(model);

                ViewBag.ErrorMessage = model!.FirstOrDefault()!.Message;
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
                var updateBatchDto = new UpdateBatchDto
                {
                    BatchNumber = model!.BatchNumber,
                    BatchQuantity = model.BatchQuantity,
                    Category = model.Category,
                    ExpiryDate = model.ExpiryDate,
                    MedicineId = model.MedicineId,
                    PurchasePrice = model.PurchasePrice
                };

                var medicines = await _medicineService.GetAllMedicinesAsync();
                var safeMedicines = medicines ?? Enumerable.Empty<GetMedicineDto>();
                ViewBag.Medicines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(safeMedicines, "MedicineId", "Name");
                ViewBag.MedicineCategoriesJson = System.Text.Json.JsonSerializer.Serialize(safeMedicines.ToDictionary(m => m.MedicineId.ToString(), m => (int)m.Category));

                return View(updateBatchDto);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> EditBatch(int id, UpdateBatchDto model)
        {
            if (ModelState.IsValid)
            {
                var newData = await _batchService.UpdateBatchAsync(id, model)!;
                if (newData != null)
                {
                    TempData["SuccessMessage"] = "Update batch date done successfully!";
                    return RedirectToAction("ManageBatchs");
                }

                TempData["ErrorMessage"] = newData!.Message;
                return RedirectToAction("ManageBatchs");
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
            var result = await _batchService.DeleteBatchAsync(id)!;
            if (!result)
            {
                TempData["ErrorMessage"] = "Delete an existing batch faild, please try again later";
                return RedirectToAction("ManageBatchs");
            }

            TempData["SuccessMessage"] = "Delete existing batch done successfully!";
            return RedirectToAction("ManageBatchs");
        }

        [HttpGet]
        public async Task<IActionResult> CreateBatch()
        {
            if (_sessionHelper.IsAuthenticated() && _sessionHelper.IsInRole("Admin"))
            {
                var medicines = await _medicineService.GetAllMedicinesAsync();
                var safeMedicines = medicines ?? Enumerable.Empty<GetMedicineDto>();
                ViewBag.Medicines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(safeMedicines, "MedicineId", "Name");
                ViewBag.MedicineCategoriesJson = System.Text.Json.JsonSerializer.Serialize(safeMedicines.ToDictionary(m => m.MedicineId.ToString(), m => (int)m.Category));
                return View(new CreateBatchDto());
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBatch(CreateBatchDto model)
        {
            if (ModelState.IsValid)
            {
                var createdbatch = await _batchService.CreateBatchAsync(model)!;
                if (createdbatch.BatchId != 0)
                {
                    TempData["SuccessMessage"] = "Adding a new batch done successfully!";
                    return RedirectToAction("ManageBatchs");
                }

                TempData["ErrorMessage"] = createdbatch!.Message;
                return RedirectToAction("ManageBatchs");
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
                if (model == null || !model.Any())
                {
                    return View(new List<GetSaleDto>());
                }

                var first = model.FirstOrDefault();
                if (first != null && !string.IsNullOrEmpty(first.Message) && first.SaleId == 0)
                {
                    ViewBag.ErrorMessage = first.Message;
                    return View(model);
                }

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
                if (model != null && model.FirstOrDefault()!.Message == "")
                    return View(model);

                ViewBag.ErrorMessage = model!.FirstOrDefault()!.Message;
                return View(model);
            }
            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            if (!_sessionHelper.IsAuthenticated() || !_sessionHelper.IsInRole("Admin"))
                return RedirectToAction("Login", "Accounts");

            try
            {
                var viewModel = await _reportsService.BuildReportsViewModelAsync();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
