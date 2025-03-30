using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.ProjectAggregate.Enums;

namespace SmartInventoryBE.Models;

public class SmartInventoryContext : IdentityDbContext<User>
{
    public SmartInventoryContext(DbContextOptions<SmartInventoryContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<PaymentTransaction> PaymentTransaction { get; set; }
    public DbSet<Shipping> Shipping { get; set; }

    protected override void OnModelCreating(ModelBuilder buiilder)
    {
        base.OnModelCreating(buiilder);

        var roles = new List<IdentityRole>()
        {
            new()
            {
                Name = Role.Admin.ToString(),
                NormalizedName = Role.Admin.ToString().ToUpper()
            },
            new()
            {
                Name = Role.User.ToString(),
                NormalizedName = Role.User.ToString().ToUpper()
            }
        };
        buiilder.Entity<IdentityRole>().HasData(roles);
    }

}
