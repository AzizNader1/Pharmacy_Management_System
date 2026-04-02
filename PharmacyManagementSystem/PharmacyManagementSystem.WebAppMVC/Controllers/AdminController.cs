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
                if (model != null && model.FirstOrDefault()!.Message == "")
                    return View(model);

                ViewBag.ErrorMessage = model!.FirstOrDefault()!.Message;
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
            {
                return RedirectToAction("Login", "Accounts");
            }

            var viewModel = new ReportsViewModel();

            try
            {
                // 1. Fetch all sales with sale items (full data)
                var allSales = await _saleService.GetAllSalesAsync();
                var allSaleItems = await _saleItemService.GetAllSaleItemesAsync();
                var allMedicines = await _medicineService.GetAllMedicinesAsync();
                var allUsers = await _userService.GetAllUsersAsync();

                // Filter out error responses (SaleId == 0 means error DTO)
                var validSales = allSales?.Where(s => s != null && s.SaleId != 0).ToList() ?? [];
                var validSaleItems = allSaleItems?.Where(si => si != null && si.SaleItemId != 0).ToList() ?? [];
                var validMedicines = allMedicines?.Where(m => m != null && m.MedicineId != 0).ToList() ?? [];
                var validUsers = allUsers?.Where(u => u != null && u.UserId != 0).ToList() ?? [];

                // ====== KPIs ======
                viewModel.TotalRevenue = validSales.Sum(s => s!.TotalAmount);
                viewModel.TotalOrders = validSales.Count;
                viewModel.AverageOrderValue = viewModel.TotalOrders > 0
                    ? Math.Round(viewModel.TotalRevenue / viewModel.TotalOrders, 2)
                    : 0;
                viewModel.TotalItemsSold = validSaleItems.Sum(si => si!.ItemQuantity);
                viewModel.UniqueMedicinesSold = validSaleItems
                    .Select(si => si!.MedicineId).Distinct().Count();

                // ====== Daily Revenue (Last 7 Days) ======
                var today = DateTime.Today;
                for (int i = 6; i >= 0; i--)
                {
                    var date = today.AddDays(-i);
                    var daySales = validSales.Where(s => s!.SalesDate.Date == date).ToList();
                    viewModel.DailyRevenue.Add(new RevenueByPeriod
                    {
                        Label = date.ToString("MMM dd"),
                        Revenue = daySales.Sum(s => s!.TotalAmount),
                        OrderCount = daySales.Count
                    });
                }

                // ====== Weekly Revenue (Last 8 Weeks) ======
                // Find the start of the current week (Monday)
                var currentWeekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                if ((int)today.DayOfWeek == 0) currentWeekStart = today.AddDays(-6); // Sunday fix

                for (int i = 7; i >= 0; i--)
                {
                    var weekStart = currentWeekStart.AddDays(-7 * i);
                    var weekEnd = weekStart.AddDays(7);
                    var weekSales = validSales
                        .Where(s => s!.SalesDate.Date >= weekStart && s.SalesDate.Date < weekEnd)
                        .ToList();
                    viewModel.WeeklyRevenue.Add(new RevenueByPeriod
                    {
                        Label = $"W{weekStart:MMM dd}-{weekEnd.AddDays(-1):dd}",
                        Revenue = weekSales.Sum(s => s!.TotalAmount),
                        OrderCount = weekSales.Count
                    });
                }

                // ====== Monthly Revenue (Last 6 Months) ======
                for (int i = 5; i >= 0; i--)
                {
                    var month = today.AddMonths(-i);
                    var monthSales = validSales
                        .Where(s => s!.SalesDate.Year == month.Year && s.SalesDate.Month == month.Month)
                        .ToList();
                    viewModel.MonthlyRevenue.Add(new RevenueByPeriod
                    {
                        Label = month.ToString("MMM yyyy"),
                        Revenue = monthSales.Sum(s => s!.TotalAmount),
                        OrderCount = monthSales.Count
                    });
                }

                // ====== Top Selling Medicines (Top 8) ======
                var medicineDict = validMedicines.ToDictionary(m => m!.MedicineId);
                viewModel.TopMedicines = validSaleItems
                    .GroupBy(si => si!.MedicineId)
                    .Select(g => new TopMedicineSale
                    {
                        MedicineName = medicineDict.ContainsKey(g.Key)
                            ? medicineDict[g!.Key]!.Name
                            : $"Medicine #{g.Key}",
                        QuantitySold = g.Sum(si => si!.ItemQuantity),
                        TotalRevenue = g.Sum(si => si!.TotalLinePrice)
                    })
                    .OrderByDescending(m => m.TotalRevenue)
                    .Take(8)
                    .ToList();

                // ====== Sales by Category ======
                viewModel.SalesByCategory = validSaleItems
                    .Join(validMedicines,
                        si => si!.MedicineId,
                        m => m!.MedicineId,
                        (si, m) => new { si, m!.Category })
                    .GroupBy(x => x.Category.ToString())
                    .Select(g => new CategorySale
                    {
                        Category = g.Key,
                        Revenue = g.Sum(x => x.si!.TotalLinePrice),
                        ItemsSold = g.Sum(x => x.si!.ItemQuantity)
                    })
                    .OrderByDescending(c => c.Revenue)
                    .ToList();

                // ====== Recent Transactions (Last 10) ======
                var userDict = validUsers.ToDictionary(u => u.UserId);
                viewModel.RecentTransactions = validSales
                    .OrderByDescending(s => s.SalesDate)
                    .Take(10)
                    .Select(s => new RecentTransaction
                    {
                        SaleId = s!.SaleId,
                        SalesDate = s.SalesDate,
                        TotalAmount = s.TotalAmount,
                        ItemsCount = s.SaleItems?.Count ?? 0,
                        CashierName = s.UserId != 0 && userDict.ContainsKey(s.UserId)
                            ? userDict[s.UserId]!.FullName
                            : "Unknown"
                    })
                    .ToList();

                // ====== Cashier Performance ======
                viewModel.CashierPerformances = validSales
                    .GroupBy(s => s!.UserId)
                    .Select(g =>
                    {
                        var totalRev = g.Sum(s => s!.TotalAmount);
                        var count = g.Count();
                        return new CashierPerformance
                        {
                            CashierName = g.Key != 0 && userDict.ContainsKey(g.Key)
                                ? userDict[g.Key]!.FullName
                                : $"User #{g.Key}",
                            TotalOrders = count,
                            TotalRevenue = totalRev,
                            AverageOrderValue = count > 0 ? Math.Round(totalRev / count, 2) : 0
                        };
                    })
                    .OrderByDescending(c => c.TotalRevenue)
                    .ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return View(viewModel);
        }
    }
}
