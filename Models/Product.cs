using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

[Table("Products")]
public class Product : BaseEntity
{
    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "varchar(400)")]
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public int ProductStock { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string Image { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public Inventory? Inventory { get; set; }
    public List<OrderProduct> OrderDetails { get; set; } = new List<OrderProduct>();
    public List<Review> Reviews { get; set; } = new List<Review>();
}
