using Microsoft.EntityFrameworkCore;

namespace Task2.Models;

public class NorthwindContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=127.0.0.1:3306;user=root;password=password;database=Northwind",
                new MySqlServerVersion(new Version(8, 0, 25)));
        }
    }
}