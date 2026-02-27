using FertilizerShopWeb.Models;
using Microsoft.EntityFrameworkCore;

//namespace FertilizerShopWeb.Data
//{
//    public class AppDbContext
//    {
//    }
//}


//using Fertilizer_And_Seed_management_System.Models;
//using Microsoft.EntityFrameworkCore;

namespace FertilizerShopWeb.Data
{
    public class AppDbContext : DbContext
    {


        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options) { }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Product> Products { get; set; }

        //public class AppDbContext : DbContext
        //{
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Company> Companies { get; set; }

        
      




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            // 1️⃣ Prevent cascade delete for Products -> Companies
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CompanyId)
                //.OnDelete(DeleteBehavior.Cascade);
                .OnDelete(DeleteBehavior.Restrict);// 👈 important


            // Price precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);   // 👈 fix
        }




    }

}


