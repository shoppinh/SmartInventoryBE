namespace SmartInventoryBE.ProjectAggregate.Response;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Image { get; set; } = string.Empty;
    public required CategoryResponse Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // public Category Category { get; set; }
    // public Inventory Inventory { get; set; }
    // public List<OrderProduct> OrderDetails { get; set; } = new List<OrderProduct>();
    // public List<Review> Reviews { get; set; } = new List<Review>();
} 