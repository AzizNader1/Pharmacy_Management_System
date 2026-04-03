using PharmacyManagementSystem.WebAppMVC.Models;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly IApiSaleServices _saleService;
        private readonly IApiSaleItemsServices _saleItemService;
        private readonly IApiMedicineServices _medicineService;
        private readonly IApiUserServices _userService;

        public ReportsService(
            IApiSaleServices saleService,
            IApiSaleItemsServices saleItemService,
            IApiMedicineServices medicineService,
            IApiUserServices userService)
        {
            _saleService = saleService;
            _saleItemService = saleItemService;
            _medicineService = medicineService;
            _userService = userService;
        }

        public async Task<ReportsViewModel> BuildReportsViewModelAsync()
        {
            var viewModel = new ReportsViewModel();

            var allSales = await _saleService.GetAllSalesAsync();
            var allSaleItems = await _saleItemService.GetAllSaleItemesAsync();
            var allMedicines = await _medicineService.GetAllMedicinesAsync();
            var allUsers = await _userService.GetAllUsersAsync();

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
            var currentWeekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            if ((int)today.DayOfWeek == 0) currentWeekStart = today.AddDays(-6);

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

            return viewModel;
        }
    }
}
