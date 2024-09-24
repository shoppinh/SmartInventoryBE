using Microsoft.EntityFrameworkCore;

namespace SmartInventoryBE.Models;

public class SmartInventoryContext : DbContext
{
    public SmartInventoryContext(DbContextOptions<SmartInventoryContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItems { get; set; } = null;
    public DbSet<Stock> Stocks { get; set; } = null;
    public DbSet<Comment> Comments { get; set; } = null;
}