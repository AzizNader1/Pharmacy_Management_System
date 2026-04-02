namespace PharmacyManagementSystem.WebAppMVC.Models
{
    public class ReportsViewModel
    {
        // ===== Summary KPIs =====
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int TotalItemsSold { get; set; }
        public int UniqueMedicinesSold { get; set; }

        // ===== Revenue by Period (for line/bar charts) =====
        public List<RevenueByPeriod> DailyRevenue { get; set; } = [];
        public List<RevenueByPeriod> WeeklyRevenue { get; set; } = [];
        public List<RevenueByPeriod> MonthlyRevenue { get; set; } = [];

        // ===== Top Selling Medicines (for bar chart) =====
        public List<TopMedicineSale> TopMedicines { get; set; } = [];

        // ===== Sales by Category (for doughnut chart) =====
        public List<CategorySale> SalesByCategory { get; set; } = [];

        // ===== Recent Transactions (for table) =====
        public List<RecentTransaction> RecentTransactions { get; set; } = [];

        // ===== Cashier Performance (for horizontal bar chart) =====
        public List<CashierPerformance> CashierPerformances { get; set; } = [];
    }

    // ---- Sub-models ----

    public class RevenueByPeriod
    {
        public string Label { get; set; } = "";
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
    }

    public class TopMedicineSale
    {
        public string MedicineName { get; set; } = "";
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class CategorySale
    {
        public string Category { get; set; } = "";
        public decimal Revenue { get; set; }
        public int ItemsSold { get; set; }
    }

    public class RecentTransaction
    {
        public int SaleId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ItemsCount { get; set; }
        public string? CashierName { get; set; }
    }

    public class CashierPerformance
    {
        public string CashierName { get; set; } = "";
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
}
