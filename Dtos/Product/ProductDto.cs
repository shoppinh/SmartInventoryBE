using System;
using SmartInventoryBE.Dtos.Category;

namespace SmartInventoryBE.Dtos.Product;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public required CategoryDto Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // public Category Category { get; set; }
    // public Inventory Inventory { get; set; }
    // public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    // public List<Review> Reviews { get; set; } = new List<Review>();
}
