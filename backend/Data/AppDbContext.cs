using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users {get;set;}
    public DbSet<Company> Companies { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed some example companies
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id                 = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name               = "IBO Finance",
                Description        = "International banking operations. USD only.",
                AllowedCurrencies  = "USD",
                MinAmount          = 10,
                MaxAmount          = 50000,
                IsActive           = true
            },
            new Company
            {
                Id                 = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name               = "EuroTrans",
                Description        = "European transfers. EUR and RUB accepted.",
                AllowedCurrencies  = "EUR,RUB",
                MinAmount          = 5,
                MaxAmount          = 20000,
                IsActive           = true
            },
            new Company
            {
                Id                 = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name               = "GlobalPay",
                Description        = "Global payments. All currencies accepted.",
                AllowedCurrencies  = "USD,EUR,RUB",
                MinAmount          = 1,
                MaxAmount          = 100000,
                IsActive           = true
            }
        );
    }
}