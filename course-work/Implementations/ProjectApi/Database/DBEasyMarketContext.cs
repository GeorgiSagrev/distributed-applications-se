using Microsoft.EntityFrameworkCore;
using ProjectApi.Entities;

namespace ProjectApi.Database
{
    public class DBEasyMarketContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Worker>()
            //        .HasIndex(worker => worker.EmailAddress)
            //        .IsUnique();
        }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SAGREVPC\\SQLEXPRESS;Database=EasyMarketDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}