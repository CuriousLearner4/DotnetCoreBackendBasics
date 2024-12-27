using Microsoft.EntityFrameworkCore;
using EF.Models;

namespace EF.Data
{
    public class PizzaContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;   

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-TMNL352O\SQLEXPRESS01;Database=PizzaDB;Trusted_Connection=True;");
        }
    }
}
