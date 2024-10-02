using System;

namespace SmartInventoryBE.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
