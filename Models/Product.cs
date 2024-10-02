using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public Inventory? Inventory { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public List<Review> Reviews { get; set; } = new List<Review>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
