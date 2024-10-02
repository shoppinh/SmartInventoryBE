using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Models;

public class SmartInventoryContext : DbContext
{
    public SmartInventoryContext(DbContextOptions<SmartInventoryContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Log> Logs { get; set; }

public DbSet<SmartInventoryBE.Models.PaymentTransaction> PaymentTransaction { get; set; } = default!;

public DbSet<SmartInventoryBE.Models.Shipping> Shipping { get; set; } = default!;

}
