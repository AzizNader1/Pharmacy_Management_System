using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. USER CONFIGURATION
            // ==========================================

            // User -> Sale
            // Rule: If a User is deleted, DO NOT delete their Sales history.
            // We want to keep the record of who made the sale.
            modelBuilder.Entity<User>()
                .HasMany(u => u.Sales)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // 2. MEDICINE CONFIGURATION
            // ==========================================

            // Medicine -> Batch
            // Rule: If a Medicine is deleted, DO NOT delete Batches automatically.
            // (Safety measure to prevent accidental stock wipe).
            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.Batches)
                .WithOne(b => b.Medicine)
                .HasForeignKey(b => b.MedicineId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // 3. BATCH CONFIGURATION
            // ==========================================

            // Batch -> SaleItems
            // Rule: If a Batch is deleted, DO NOT delete SaleItems linked to it.
            // (Preserves sales history).
            modelBuilder.Entity<Batch>()
                .HasMany(b => b.SaleItems)
                .WithOne(si => si.Batch)
                .HasForeignKey(si => si.BatchId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // 4. SALE CONFIGURATION
            // ==========================================

            // Sale -> SaleItems
            // Rule: If a Sale is deleted (cancelled), DELETE the items too.
            // This is the ONLY Cascade Delete we allow.
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // 5. SALEITEM CONFIGURATION (The Critical Fix)
            // ==========================================

            // Medicine -> SaleItem
            // Rule: If a Medicine is deleted, DO NOT delete SaleItems.
            // This fixes the "Multiple Cascade Path" error.
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Medicine)
                .WithMany() // We don't need a collection of SaleItems in Medicine entity
                .HasForeignKey(si => si.MedicineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}