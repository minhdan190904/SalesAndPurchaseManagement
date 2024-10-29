using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Models;

namespace SalesAndPurchaseManagement.Data
{
    public class SAPManagementContext : DbContext
    {
        public SAPManagementContext(DbContextOptions<SAPManagementContext> options) : base(options)
        {
            Database.EnsureCreated();
            SeedDatabase();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CountryOfOrigin> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Job)
                .WithMany(j => j.Employees)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Shape)
                .WithMany(sh => sh.Products)
                .HasForeignKey(p => p.ShapeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Material)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CountryOfOriginId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Feature)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Characteristic)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.CharacteristicId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void SeedDatabase()
        {
            if (!Jobs.Any())
            {
                var managerJob = new Job
                {
                    JobTitle = "Quản lý",
                    Salary = 15000000
                };
                Jobs.Add(managerJob);
                SaveChanges();

                var adminEmployee = new Employee
                {
                    EmployeeName = "Admin",
                    Gender = Gender.Male,
                    Email = "hungbgclone01@gmail.com",
                    Password = "tyquay2004",
                    PhoneNumber = "0363686126",
                    Address = "Hà Nội",
                    DateOfBirth = new DateTime(2004, 5, 15),
                    IsAdmin = true,
                    JobId = managerJob.JobId,
                    Image = "user_default.png"
                };
                Employees.Add(adminEmployee);
                SaveChanges();
            }
        }
    }
}
